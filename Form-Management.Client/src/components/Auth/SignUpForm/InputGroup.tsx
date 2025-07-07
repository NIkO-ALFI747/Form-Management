import { type ChangeEvent, type Dispatch, type FC, type RefObject, type SetStateAction } from 'react'
import * as formik from 'formik'
import Name from '../Inputs/Name.tsx'
import Password from '../Inputs/Password.tsx'
import Email from '../Inputs/Email.tsx'

interface InputGroupProps {
  passwordErrorMessage: string | null
  setPasswordErrorMessage: Dispatch<SetStateAction<string | null>>
  emailErrorMessage: string | null
  setEmailErrorMessage: Dispatch<SetStateAction<string | null>>
  handleChange: {
    (e: ChangeEvent<any>): void
    <T = string | ChangeEvent<any>>(field: T): T extends ChangeEvent<any> ? void : (e: string | ChangeEvent<any>) => void
  }
  handleBlur: {
    (e: React.FocusEvent<any>): void;
    <T = any>(fieldOrEvent: T): T extends string ? (e: any) => void : void;
  }
  setFieldTouched: (field: string, isTouched?: boolean, shouldValidate?: boolean) => Promise<void | formik.FormikErrors<{
    name: string;
    email: string;
    password: string;
  }>>
  touched: formik.FormikTouched<{
    name: string;
    email: string;
    password: string;
  }>
  values: {
    name: string;
    email: string;
    password: string;
  }
  errors: formik.FormikErrors<{
    name: string;
    email: string;
    password: string;
  }>
  isSubmitted: boolean
  setSubmitted: Dispatch<SetStateAction<boolean>>
  isBlur: boolean
  setIsBlur: Dispatch<SetStateAction<boolean>>
  showErrMessages: boolean
  setShowErrMessages: Dispatch<SetStateAction<boolean>>
  isSubmittingHandled: RefObject<boolean>
  isInvalid: boolean
  setIsInvalid: Dispatch<SetStateAction<boolean>>
  submitCount: number
  isValid: boolean
  lastSubmitCount: RefObject<number>
  isBlurHandled: RefObject<boolean>
  invalidPasswordRef: RefObject<string | null>
  isSubmitting: boolean
}

const InputGroup: FC<InputGroupProps> = ({
  passwordErrorMessage,
  setPasswordErrorMessage,
  emailErrorMessage,
  setEmailErrorMessage,
  handleChange,
  handleBlur,
  setFieldTouched,
  touched, values, errors,
  isSubmitted,
  setSubmitted,
  isBlur,
  setIsBlur,
  showErrMessages,
  setShowErrMessages,
  isSubmittingHandled,
  isInvalid,
  setIsInvalid,
  submitCount,
  isValid,
  lastSubmitCount,
  isBlurHandled,
  invalidPasswordRef,
  isSubmitting
}) => {
  return (
    <>
      <Name name={values.name} onChangeName={handleChange} />
      <Email 
      email={values.email} 
      onChangeEmail={handleChange} 
      errorMessage={emailErrorMessage}
      setErrorMessage={setEmailErrorMessage}
      />
      <Password password={values.password}
        onChangePassword={handleChange}
        handleBlur={handleBlur}
        setFieldTouched={setFieldTouched}
        touched={touched} errors={errors}
        errorMessage={passwordErrorMessage}
        setErrorMessage={setPasswordErrorMessage}
        isSubmitted={isSubmitted}
        setSubmitted={setSubmitted}
        isBlur={isBlur}
        setIsBlur={setIsBlur}
        showErrMessages={showErrMessages}
        setShowErrMessages={setShowErrMessages}
        isSubmittingHandled={isSubmittingHandled}
        isInvalid={isInvalid}
        setIsInvalid={setIsInvalid}
        submitCount={submitCount}
        isValid={isValid}
        lastSubmitCount={lastSubmitCount}
        isBlurHandled={isBlurHandled}
        invalidPasswordRef={invalidPasswordRef}
        isSubmitting={isSubmitting}
      />
    </>
  )
}

export default InputGroup