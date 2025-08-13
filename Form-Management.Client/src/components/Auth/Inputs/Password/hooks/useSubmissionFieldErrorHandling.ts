import type { FormikProps, FormikValues } from "formik";
import { useCallback, useEffect, useRef, type RefObject } from "react";
import type { UseAuthFormFieldReturn } from "../../../hooks/useAuthFormField";

interface UseSubmissionFieldErrorHandlingProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormField: UseAuthFormFieldReturn;
  currentFieldServerErr: RefObject<string>;
}

export const useSubmissionFieldErrorHandling = <TFormik extends FormikValues>({
  formik,
  authFormField,
  currentFieldServerErr,
}: UseSubmissionFieldErrorHandlingProps<TFormik>) => {
  const lastSubmitCount = useRef(formik.submitCount);

  const setAllErrorVisibilityFlags = useCallback(
    (
      isInvalidFlag: boolean = true,
      showMessagesFlag: boolean = true,
      showServerErrFlag: boolean = true
    ) => {
      authFormField.setIsInvalid(isInvalidFlag);
      authFormField.setShowErrMessages(showMessagesFlag);
      authFormField.setShowServerErrMessage(showServerErrFlag);
    },
    [authFormField]
  );

  const manageClientSideServerErrorVisibility = useCallback(() => {
    if (
      formik.values[authFormField.elementName.toLowerCase()] ===
      authFormField.invalidFieldRef.current
    )
      setAllErrorVisibilityFlags();
    else authFormField.setShowServerErrMessage(false);
  }, [formik, authFormField, setAllErrorVisibilityFlags]);

  const handleClientSideValidationError = useCallback(() => {
    manageClientSideServerErrorVisibility();
    const hasFormikErrors =
      !!formik.errors[authFormField.elementName.toLowerCase()];
    authFormField.setIsInvalid(hasFormikErrors);
    authFormField.setShowErrMessages(hasFormikErrors);
    lastSubmitCount.current = formik.submitCount;
  }, [formik, manageClientSideServerErrorVisibility, authFormField]);

  const handleServerSideSubmissionError = useCallback(() => {
    if (authFormField.serverErrorMessage !== null) {
      currentFieldServerErr.current = authFormField.serverErrorMessage;
      authFormField.invalidFieldRef.current =
        formik.values[authFormField.elementName.toLowerCase()];
      setAllErrorVisibilityFlags();
      lastSubmitCount.current = formik.submitCount;
    }
  }, [formik, authFormField, setAllErrorVisibilityFlags]);

  useEffect(() => {
    const hasNewSubmitAttempt = formik.submitCount > lastSubmitCount.current;
    if (!hasNewSubmitAttempt) return;
    if (!formik.isValid) handleClientSideValidationError();
    else if (!formik.isSubmitting) handleServerSideSubmissionError();
  }, [
    formik,
    handleClientSideValidationError,
    handleServerSideSubmissionError,
  ]);
};
