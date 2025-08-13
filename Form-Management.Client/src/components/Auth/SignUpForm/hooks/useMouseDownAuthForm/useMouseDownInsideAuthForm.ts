import type { FormikProps, FormikValues } from "formik";
import { useCallback, type MouseEvent as ReactMouseEvent } from "react";
import type { AuthFormField } from "./useMouseDownAuthForm";

interface UseMouseDownInsideAuthFormProps<TField extends AuthFormField> {
  authFormFields: TField[];
  submitButtonId: string;
}

const isInputElement = (
  element: HTMLElement,
  inputElementId: string
): boolean =>
  element instanceof HTMLInputElement && element.id === inputElementId;

const isSubmitButton = (
  element: HTMLElement,
  submitButtonId: string
): boolean =>
  element instanceof HTMLButtonElement && element.id === submitButtonId;

const hasFieldError = (
  formikFieldError: string | null,
  showServerErrMessage: boolean
): boolean => !!formikFieldError || showServerErrMessage;

export const useMouseDownInsideAuthForm = <TField extends AuthFormField>({
  authFormFields,
  submitButtonId,
}: UseMouseDownInsideAuthFormProps<TField>) => {
  const isOneOfInputElements = (
    element: HTMLElement,
    authFormFields: TField[]
  ): TField | null => {
    const matches = authFormFields.filter((field) =>
      isInputElement(element, field.inputElementId)
    );
    if (matches.length > 1) throw new Error(`Invalid authFormFields!`);
    return matches.length === 1 ? matches[0] : null;
  };

  const isFormEmpty = <TFormik extends FormikValues>(
    formik: FormikProps<TFormik>
  ): boolean => Object.keys(formik.values).every((key) => !formik.values[key]);

  const handleEmptyFormValidation = useCallback(
    <TFormik extends FormikValues>(
      formik: FormikProps<TFormik>,
      hasFieldError: boolean,
      clickedField: TField | null,
      field: TField
    ) => {
      clickedField !== null && field.elementName === clickedField.elementName
        ? clickedField.setIsInvalid(
            !!formik.touched[field.elementName.toLowerCase()] && hasFieldError
          )
        : field.setIsInvalid(false);
    },
    [authFormFields]
  );

  const handleFilledFormValidation = <TFormik extends FormikValues>(
    formik: FormikProps<TFormik>,
    hasFieldError: boolean,
    field: TField
  ) => {
    field.setIsInvalid(hasFieldError);
    if (!formik.touched[field.elementName.toLowerCase()])
      formik.setFieldTouched(field.elementName.toLowerCase(), true);
  };

  const updateValidationState = useCallback(
    <TFormik extends FormikValues>(
      clickedField: TField | null,
      formik: FormikProps<TFormik>
    ) => {
      const numFields = authFormFields.length;
      const hasFieldsError = authFormFields.map((field) => {
        const matches = Object.keys(formik.errors).filter(
          (formikErrorField) =>
            formikErrorField === field.elementName.toLowerCase()
        );
        if (matches.length > 1)
          throw new Error(`Invalid authFormFields or formik.errors!`);
        const formikErrorField = matches.length === 1 ? matches[0] : null;
        return hasFieldError(formikErrorField, field.showServerErrMessage);
      });
      if (isFormEmpty(formik)) {
        for (let i = 0; i < numFields; i++)
          handleEmptyFormValidation(
            formik,
            hasFieldsError[i],
            clickedField,
            authFormFields[i]
          );
      } else {
        for (let i = 0; i < numFields; i++)
          handleFilledFormValidation(
            formik,
            hasFieldsError[i],
            authFormFields[i]
          );
      }
    },
    [authFormFields, handleEmptyFormValidation, handleFilledFormValidation]
  );

  const updateErrorMessagesVisibility = useCallback(
    (clickedField: TField | null) => {
      authFormFields.forEach((field) => {
        clickedField !== null && field.elementName === clickedField.elementName
          ? clickedField.setShowErrMessages(true)
          : field.setShowErrMessages(false);
      });
    },
    [authFormFields]
  );

  const handleMouseDownInsideForm = useCallback(
    <TFormik extends FormikValues>(
      event: ReactMouseEvent<HTMLDivElement>,
      formik: FormikProps<TFormik>
    ) => {
      const clickedElement = event.target as HTMLElement;
      if (isSubmitButton(clickedElement, submitButtonId)) return;
      const clickedField = isOneOfInputElements(clickedElement, authFormFields);
      updateValidationState(clickedField, formik);
      updateErrorMessagesVisibility(clickedField);
    },
    [updateValidationState, updateErrorMessagesVisibility]
  );
  return { handleMouseDownInsideForm };
};
