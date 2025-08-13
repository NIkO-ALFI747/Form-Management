import { AxiosError } from "axios";
import type {
  AuthValidationErrors,
  EmailConflictError,
  ErrorResponse,
} from "../../types/AuthForm.ts";
import type { AuthForm, AuthFormField } from "./useSubmitAuthForm.ts";

interface UseAuthErrorHandlingProps<
  TField extends AuthFormField,
  TForm extends AuthForm
> {
  authFormFields: TField[];
  authForm: TForm;
}

export const useAuthErrorHandling = <
  TField extends AuthFormField,
  TForm extends AuthForm
>({
  authFormFields,
  authForm,
}: UseAuthErrorHandlingProps<TField, TForm>) => {
  const processValidationMessages = (
    errors: { internalError: { message: string } }[] | undefined,
    setter: (message: string) => void,
    defaultMessage: string
  ) => {
    if (errors !== undefined) {
      const messages = errors
        .map((el) => el.internalError.message)
        .filter(Boolean);
      setter(messages.length > 0 ? messages.join("\n") : defaultMessage);
    }
  };

  const handleValidationErrors = (validationsErrors: AuthValidationErrors) => {
    Object.entries(validationsErrors).forEach(([key, value]) => {
      const matches = authFormFields.filter(
        (field) => field.elementName === key
      );
      const authFormField = matches.length === 1 ? matches[0] : null;
      if (authFormField === null)
        throw new Error(`Invalid authFormFields or validationsErrors!`);
      processValidationMessages(
        value,
        authFormField.setServerErrorMessage,
        `Please provide a valid ${key}.`
      );
    });
  };

  const handleEmailConflictError = (emailConflictError: EmailConflictError) => {
    const matches = authFormFields.filter(
      (field) => field.elementName === "Email"
    );
    const authFormField = matches.length === 1 ? matches[0] : null;
    if (authFormField === null) throw new Error(`Invalid authFormFields!`);
    authFormField.setServerErrorMessage(emailConflictError.message);
  };

  const handleGenericError = (errorTitle: string) => {
    if (errorTitle !== "") authForm.setAlertTitle(errorTitle);
  };

  const tryHandleValidationErrors = (errorMessages: ErrorResponse): boolean => {
    const validationErrorMessages = errorMessages.errors as
      | { validationsErrors: AuthValidationErrors }
      | undefined;
    if (validationErrorMessages?.validationsErrors) {
      handleValidationErrors(validationErrorMessages.validationsErrors);
      return true;
    }
    return false;
  };

  const tryHandleEmailConflict = (errorMessages: ErrorResponse): boolean => {
    const emailConflictMessage = errorMessages.errors as
      | { error: EmailConflictError }
      | undefined;
    if (emailConflictMessage?.error.type == 3) {
      handleEmailConflictError(emailConflictMessage.error);
      return true;
    }
    return false;
  };

  const dispatchAuthError = (errorMessages: ErrorResponse) => {
    if (tryHandleValidationErrors(errorMessages)) return;
    if (tryHandleEmailConflict(errorMessages)) return;
    handleGenericError(errorMessages.title);
  };

  const handleAuthError = (e: AxiosError) => {
    const errorMessages = e.response?.data as ErrorResponse | undefined;
    if (errorMessages) {
      dispatchAuthError(errorMessages);
      authForm.setSuccessfulAuth(false);
    }
  };

  const resetAuthErrors = () => {
    authFormFields.forEach((field) => {
      field.invalidFieldRef.current = null;
      field.setServerErrorMessage(null);
    });
  };
  return { handleAuthError, resetAuthErrors };
};
