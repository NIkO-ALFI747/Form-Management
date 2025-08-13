import type { Dispatch, SetStateAction } from "react";

export interface AuthFormProps {
  setIsAuth: Dispatch<SetStateAction<boolean>>;
}

export interface ValidationError {
  internalError: {
    message: string;
  };
}

export type ValidationErrors<TFields extends string = string> = {
  [K in TFields]?: ValidationError[];
};

type SignUpValidationErrorsResponseKeys = "Email" | "Password" | "Name";

type LoginValidationErrorsResponseKeys = "Email" | "Password";

export type SignUpValidationErrors = ValidationErrors<SignUpValidationErrorsResponseKeys>;

export type LoginValidationErrors = ValidationErrors<LoginValidationErrorsResponseKeys>;

export type AuthValidationErrors = ValidationErrors<SignUpValidationErrorsResponseKeys | LoginValidationErrorsResponseKeys>;

export interface EmailConflictError {
  message: string;
  type: 3;
}

export interface ErrorResponse {
  title: string;
  errors:
    | {
        validationsErrors: AuthValidationErrors;
      }
    | {
        error: EmailConflictError;
      }
    | null;
}
