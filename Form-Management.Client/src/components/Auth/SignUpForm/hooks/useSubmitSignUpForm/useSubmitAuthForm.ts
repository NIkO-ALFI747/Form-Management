import {
  useCallback,
  type Dispatch,
  type RefObject,
  type SetStateAction,
} from "react";
import type { FormikState, FormikValues } from "formik";
import { AxiosError } from "axios";
import { useAuthErrorHandling } from "./useAuthErrorHandling.ts";

export interface AuthFormField {
  setServerErrorMessage: (message: string | null) => void;
  invalidFieldRef: RefObject<string | null>;
  elementName: string;
}

export interface AuthForm {
  setSuccessfulAuth: Dispatch<SetStateAction<boolean>>;
  setAlertTitle: Dispatch<SetStateAction<string>>;
}

interface UseSubmitAuthFormProps<
  TField extends AuthFormField,
  TForm extends AuthForm,
  TFormik extends FormikValues
> {
  setIsAuth: (isAuth: boolean) => void;
  authFormFields: TField[];
  authForm: TForm;
  authUserService: (user: TFormik, role?: string) => Promise<any>;
}

export const useSubmitAuthForm = <
  TField extends AuthFormField,
  TForm extends AuthForm,
  TFormik extends FormikValues
>({
  setIsAuth,
  authFormFields,
  authForm,
  authUserService,
}: UseSubmitAuthFormProps<TField, TForm, TFormik>) => {
  const { handleAuthError, resetAuthErrors } = useAuthErrorHandling<
    TField,
    TForm
  >({
    authFormFields,
    authForm,
  });

  const authUser = async (
    user: TFormik,
    setSubmitting: (isSubmitting: boolean) => void,
    resetForm: (nextState?: Partial<FormikState<TFormik>> | undefined) => void,
    role?: string
  ) => {
    try {
      await authUserService(user, role);
      setIsAuth(true);
      resetForm();
    } catch (e) {
      handleAuthError(e as AxiosError);
    } finally {
      setSubmitting(false);
    }
  };

  const onSubmitFormik = useCallback(
    (
      user: TFormik,
      setSubmitting: (isSubmitting: boolean) => void,
      resetForm: (
        nextState?: Partial<FormikState<TFormik>> | undefined
      ) => void,
      role?: string
    ) => {
      const areSomeFormFieldsInvalid = authFormFields.some(
        (field) =>
          field.invalidFieldRef.current ===
          user[field.elementName.toLowerCase()]
      );
      if (areSomeFormFieldsInvalid) {
        setSubmitting(false);
      } else {
        resetAuthErrors();
        authUser(user, setSubmitting, resetForm, role);
      }
    },
    [authFormFields, resetAuthErrors, authUser]
  );

  return { onSubmitFormik, authForm };
};
