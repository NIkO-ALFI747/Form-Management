import { useFormik } from "formik";
import type { FormikProps, FormikState, FormikValues } from "formik";
import * as yup from "yup";

interface UseAuthFormikProps<
  TFormik extends FormikValues,
  TSchema extends yup.AnyObjectSchema
> {
  onSubmitFormik: (
    user: TFormik,
    setSubmitting: (isSubmitting: boolean) => void,
    resetForm: (nextState?: Partial<FormikState<TFormik>> | undefined) => void,
    selectedRole?: string
  ) => void;
  initialValues: TFormik;
  validationSchema: TSchema;
  selectedRole?: string;
}

interface UseAuthFormikReturn<T> {
  formik: FormikProps<T>;
}

export const useAuthFormik = <
  TFormik extends FormikValues,
  TSchema extends yup.AnyObjectSchema
>({
  onSubmitFormik,
  initialValues,
  validationSchema,
  selectedRole
}: UseAuthFormikProps<TFormik, TSchema>): UseAuthFormikReturn<TFormik> => {
  const formik = useFormik<TFormik>({
    initialValues,
    validationSchema,
    onSubmit: (values, { setSubmitting, resetForm }) => {
      onSubmitFormik(values, setSubmitting, resetForm, selectedRole);
    },
  });

  return {
    formik,
  };
};
