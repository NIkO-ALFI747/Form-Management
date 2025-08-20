import type { FormikState, FormikValues } from "formik";
import type { UseAuthFormReturn } from "../hooks/useAuthForm";
import type { UseAuthFormFieldReturn } from "../hooks/useAuthFormField";
import * as yup from "yup";

export interface AuthFormProps<
  TFormik extends FormikValues,
  TSchema extends yup.AnyObjectSchema
> {
  onSubmitFormik: (
    user: TFormik,
    setSubmitting: (isSubmitting: boolean) => void,
    resetForm: (nextState?: Partial<FormikState<TFormik>> | undefined) => void
  ) => void;
  initialValues: TFormik;
  authForm: UseAuthFormReturn;
  authFormFields: UseAuthFormFieldReturn[];
  submitButtonTitle: string;
  validationSchema: TSchema;
  isRoleSet?: boolean;
}
