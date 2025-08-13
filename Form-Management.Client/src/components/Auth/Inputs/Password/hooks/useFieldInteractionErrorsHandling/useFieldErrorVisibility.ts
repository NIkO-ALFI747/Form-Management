import { useCallback } from "react";
import type { FormikProps, FormikValues } from "formik";
import type { UseAuthFormFieldReturn } from "../../../../hooks/useAuthFormField";

interface UseFieldErrorVisibilityProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormField: UseAuthFormFieldReturn;
}

export const useFieldErrorVisibility = <TFormik extends FormikValues>({
  formik,
  authFormField,
}: UseFieldErrorVisibilityProps<TFormik>) => {
  const updateErrorVisibility = useCallback(() => {
    const hasFormikError =
      !!formik.errors[authFormField.elementName.toLowerCase()];
    const shouldBeInvalid =
      hasFormikError || authFormField.showServerErrMessage;
    authFormField.setIsInvalid(shouldBeInvalid);
    authFormField.setShowErrMessages(shouldBeInvalid);
  }, [formik, authFormField]);

  return { updateErrorVisibility };
};
