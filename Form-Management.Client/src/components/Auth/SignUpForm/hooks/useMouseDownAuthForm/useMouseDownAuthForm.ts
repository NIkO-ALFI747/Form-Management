import type { FormikProps, FormikTouched, FormikValues } from "formik";
import {
  useCallback,
  useEffect,
  useRef,
  useState,
  type RefObject,
} from "react";
import { useMouseDownRefObject } from "../../../../hooks/useMouseDownRefObject";
import { useMouseDownInsideAuthForm } from "./useMouseDownInsideAuthForm";

export interface AuthFormField {
  setIsInvalid: (isInvalid: boolean) => void;
  setShowErrMessages: (show: boolean) => void;
  showServerErrMessage: boolean;
  inputElementId: string;
  elementName: string;
}

interface AuthForm {
  hasBlurHappenedBeforeMouseDownOutside: RefObject<boolean>;
  submitButtonId: string;
}

interface UseMouseDownAuthFormProps<
  TField extends AuthFormField,
  TForm extends AuthForm,
  TFormik extends FormikValues
> {
  authFormFields: TField[];
  authForm: TForm;
  formik: FormikProps<TFormik>;
}

export const useMouseDownAuthForm = <
  TField extends AuthFormField,
  TForm extends AuthForm,
  TFormik extends FormikValues
>({
  authFormFields,
  authForm,
  formik,
}: UseMouseDownAuthFormProps<TField, TForm, TFormik>) => {
  const { handleMouseDownInsideForm } = useMouseDownInsideAuthForm({
    authFormFields,
    submitButtonId: authForm.submitButtonId,
  });

  const mouseDownRefObject = useRef<HTMLDivElement>(null);

  const [isMouseDownOutsideRefObject, setIsMouseDownOutsideRefObject] =
    useState<boolean>(true);

  const hasMouseDownOutsideRefObjectHandled = useRef<boolean>(false);

  const handleMouseDownInsideRefObject = useCallback(() => {
    if (isMouseDownOutsideRefObject) setIsMouseDownOutsideRefObject(false);
  }, [isMouseDownOutsideRefObject]);

  const handleMouseDownOutsideRefObject = useCallback(() => {
    if (!isMouseDownOutsideRefObject) {
      hasMouseDownOutsideRefObjectHandled.current = false;
      setIsMouseDownOutsideRefObject(true);
    }
  }, [isMouseDownOutsideRefObject]);

  const handleInitialInteraction = useCallback(() => {
    hasMouseDownOutsideRefObjectHandled.current = true;
    authFormFields.forEach((field) => {
      field.setIsInvalid(false);
      field.setShowErrMessages(false);
      formik.setFieldTouched(field.elementName.toLowerCase(), false);
    });
  }, [formik, authFormFields]);

  const handleSubsequentInteraction = useCallback(() => {
    if (isMouseDownOutsideRefObject)
      authFormFields.forEach((field) =>
        formik.setFieldTouched(field.elementName.toLowerCase(), false)
      );
  }, [formik, isMouseDownOutsideRefObject, authFormFields]);

  const areSomeFieldsTouched = (
    formikTouched: FormikTouched<TFormik>
  ): boolean => Object.keys(formikTouched).some((key) => formikTouched[key]);

  useEffect(() => {
    const shouldProcessInteraction =
      authForm.hasBlurHappenedBeforeMouseDownOutside.current &&
      areSomeFieldsTouched(formik.touched);
    if (!shouldProcessInteraction) return;
    authForm.hasBlurHappenedBeforeMouseDownOutside.current = false;
    const isFirstTimeHandled = !hasMouseDownOutsideRefObjectHandled.current;
    if (isFirstTimeHandled) handleInitialInteraction();
    else handleSubsequentInteraction();
  }, [formik.touched, handleInitialInteraction, handleSubsequentInteraction]);

  useEffect(() => {
    if (
      isMouseDownOutsideRefObject &&
      !hasMouseDownOutsideRefObjectHandled.current
    ) {
      handleInitialInteraction();
    }
  }, [isMouseDownOutsideRefObject, handleInitialInteraction]);

  useMouseDownRefObject({
    mouseDownRefObject,
    handleMouseDownInsideRefObject,
    handleMouseDownOutsideRefObject,
  });

  return { handleMouseDownInsideForm, mouseDownRefObject };
};
