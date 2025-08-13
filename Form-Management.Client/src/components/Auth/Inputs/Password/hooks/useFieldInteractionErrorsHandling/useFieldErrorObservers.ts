import { useCallback, useEffect, useRef, type RefObject } from "react";
import type { FormikProps, FormikValues } from "formik";
import type { UseAuthFormFieldReturn } from "../../../../hooks/useAuthFormField";

interface UseFieldErrorObserversProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormField: UseAuthFormFieldReturn;
  currentFieldServerErr: RefObject<string>;
  prevFieldServerErr: RefObject<string>;
  handleFieldValueChange: () => void;
  isSetShowServerErrMessageOnFieldChange: boolean;
  setIsSetShowServerErrMessageOnFieldChange: (value: boolean) => void;
  updateErrorVisibility: () => void;
}

export const useFieldErrorObservers = <TFormik extends FormikValues>({
  formik,
  authFormField,
  currentFieldServerErr,
  prevFieldServerErr,
  handleFieldValueChange,
  isSetShowServerErrMessageOnFieldChange,
  setIsSetShowServerErrMessageOnFieldChange,
  updateErrorVisibility,
}: UseFieldErrorObserversProps<TFormik>) => {
  const prevFieldValue = useRef<string>(
    formik.values[authFormField.elementName.toLowerCase()]
  );
  const fieldName = authFormField.elementName.toLowerCase();
  const fieldError = formik.errors[fieldName];
  const prevFormikFieldErr = useRef<string>(
    typeof fieldError === "string" ? fieldError : ""
  );

  useEffect(() => {
    if (
      prevFieldValue.current !==
        formik.values[authFormField.elementName.toLowerCase()] &&
      formik.touched[authFormField.elementName.toLowerCase()]
    ) {
      prevFieldValue.current =
        formik.values[authFormField.elementName.toLowerCase()];
      handleFieldValueChange();
    }
  }, [formik.errors, handleFieldValueChange]);

  const handleFormikPasswordErrorChange = useCallback(() => {
    if (
      prevFormikFieldErr.current !==
      formik.errors[authFormField.elementName.toLowerCase()]
    ) {
      prevFormikFieldErr.current =
        typeof fieldError === "string" ? fieldError : "";
      updateErrorVisibility();
    }
  }, [formik, updateErrorVisibility]);

  const handleServerPasswordErrorChange = useCallback(() => {
    if (prevFieldServerErr.current !== currentFieldServerErr.current) {
      prevFieldServerErr.current = currentFieldServerErr.current;
      updateErrorVisibility();
    }
  }, [currentFieldServerErr, prevFieldServerErr, updateErrorVisibility]);

  useEffect(() => {
    if (
      isSetShowServerErrMessageOnFieldChange &&
      formik.touched[authFormField.elementName.toLowerCase()]
    ) {
      setIsSetShowServerErrMessageOnFieldChange(false);
      handleFormikPasswordErrorChange();
      handleServerPasswordErrorChange();
    }
  }, [
    formik,
    isSetShowServerErrMessageOnFieldChange,
    handleFormikPasswordErrorChange,
    handleServerPasswordErrorChange,
  ]);
};
