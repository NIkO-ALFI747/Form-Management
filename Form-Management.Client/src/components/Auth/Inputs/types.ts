import type { FormikProps, FormikValues } from "formik"
import type { UseAuthFormReturn } from "../hooks/useAuthForm";
import type { UseAuthFormFieldReturn } from "../hooks/useAuthFormField";

export interface AuthInputFieldProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormField: UseAuthFormFieldReturn;
  authForm: UseAuthFormReturn;
}
