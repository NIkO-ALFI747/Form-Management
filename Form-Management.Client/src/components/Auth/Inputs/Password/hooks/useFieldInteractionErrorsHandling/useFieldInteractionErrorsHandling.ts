import { type RefObject } from "react";
import type { FormikProps, FormikValues } from "formik";
import { useFieldServerErrorLogic } from "./useFieldServerErrorLogic";
import { useFieldErrorVisibility } from "./useFieldErrorVisibility";
import { useFieldErrorObservers } from "./useFieldErrorObservers";
import type { UseAuthFormFieldReturn } from "../../../../hooks/useAuthFormField";

interface UseFieldInteractionErrorsHandlingProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormField: UseAuthFormFieldReturn;
  currentFieldServerErr: RefObject<string>;
  prevFieldServerErr: RefObject<string>;
}

export const useFieldInteractionErrorsHandling = <
  TFormik extends FormikValues
>({
  formik,
  authFormField,
  currentFieldServerErr,
  prevFieldServerErr,
}: UseFieldInteractionErrorsHandlingProps<TFormik>) => {
  const {
    handleFieldValueChange,
    isSetShowServerErrMessageOnFieldChange,
    setIsSetShowServerErrMessageOnFieldChange,
  } = useFieldServerErrorLogic({
    formik,
    authFormField,
    currentFieldServerErr,
    prevFieldServerErr,
  });

  const { updateErrorVisibility } = useFieldErrorVisibility({
    formik,
    authFormField,
  });

  useFieldErrorObservers<TFormik>({
    formik,
    authFormField,
    currentFieldServerErr,
    prevFieldServerErr,
    handleFieldValueChange,
    isSetShowServerErrMessageOnFieldChange,
    setIsSetShowServerErrMessageOnFieldChange,
    updateErrorVisibility,
  });
};
