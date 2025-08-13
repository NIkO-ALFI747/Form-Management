import { useCallback, useState, type RefObject } from "react";
import type { FormikProps, FormikValues } from "formik";
import type { UseAuthFormFieldReturn } from "../../../../hooks/useAuthFormField";

interface UseFieldServerErrorLogicProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormField: UseAuthFormFieldReturn;
  currentFieldServerErr: RefObject<string>;
  prevFieldServerErr: RefObject<string>;
}

interface UseFieldServerErrorLogicReturn {
  handleFieldValueChange: () => void;
  isSetShowServerErrMessageOnFieldChange: boolean;
  setIsSetShowServerErrMessageOnFieldChange: (value: boolean) => void;
}

export const useFieldServerErrorLogic = <TFormik extends FormikValues>({
  formik,
  authFormField,
  currentFieldServerErr,
  prevFieldServerErr,
}: UseFieldServerErrorLogicProps<TFormik>): UseFieldServerErrorLogicReturn => {
  const [
    isSetShowServerErrMessageOnFieldChange,
    setIsSetShowServerErrMessageOnFieldChange,
  ] = useState<boolean>(false);

  const updateServerErrorState = useCallback(
    (shouldShow: boolean, currentErrorMessage: string) => {
      authFormField.setShowServerErrMessage(shouldShow);
      prevFieldServerErr.current = currentFieldServerErr.current;
      currentFieldServerErr.current = currentErrorMessage;
    },
    [authFormField, prevFieldServerErr, currentFieldServerErr]
  );

  const handleFieldValueChange = useCallback(() => {
    if (
      formik.values[authFormField.elementName.toLowerCase()] ===
        authFormField.invalidFieldRef.current &&
      authFormField.serverErrorMessage !== null
    )
      updateServerErrorState(true, authFormField.serverErrorMessage);
    else updateServerErrorState(false, "");
    setIsSetShowServerErrMessageOnFieldChange(true);
  }, [formik, authFormField, updateServerErrorState]);

  return {
    handleFieldValueChange,
    isSetShowServerErrMessageOnFieldChange,
    setIsSetShowServerErrMessageOnFieldChange,
  };
};
