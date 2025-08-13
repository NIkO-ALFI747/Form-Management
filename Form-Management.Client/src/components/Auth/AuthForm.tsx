import { type FormEvent, type JSX } from 'react'
import { Form } from 'react-bootstrap/'
import InputGroup from './SignUpForm/InputGroup.tsx'
import SubmitSection from './SubmitSection.tsx'
import type { AuthFormProps } from './types/AuthForm.ts'
import { useMouseDownAuthForm } from './SignUpForm/hooks/useMouseDownAuthForm/useMouseDownAuthForm.ts'
import type { FormikValues } from 'formik'
import { useAuthFormik } from './SignUpForm/hooks/useAuthFormik.ts'
import * as yup from "yup";

const AuthForm = <
  TFormik extends FormikValues,
  TSchema extends yup.AnyObjectSchema
>({
  onSubmitFormik,
  initialValues,
  authForm,
  authFormFields,
  submitButtonTitle,
  validationSchema
}: AuthFormProps<TFormik, TSchema>): JSX.Element => {
  const { formik } = useAuthFormik({ onSubmitFormik, initialValues, validationSchema });

  const { handleMouseDownInsideForm, mouseDownRefObject } = useMouseDownAuthForm({
    authFormFields,
    authForm,
    formik
  })

  return (
    <div
      ref={mouseDownRefObject}
      onMouseDown={(e) => handleMouseDownInsideForm(e, formik)}
    >
      <Form noValidate onSubmit={
        (e: FormEvent<HTMLFormElement>) => {
          formik.handleSubmit(e)
        }
      }>
        <InputGroup
          formik={formik}
          authFormFields={authFormFields}
          authForm={authForm}
        />
        <SubmitSection
          successfulAuth={authForm.successfulAuth}
          setSuccessfulAuth={authForm.setSuccessfulAuth}
          btnTitle={submitButtonTitle}
          alertTitle={authForm.alertTitle}
          submitCounter={formik.submitCount}
          submitButtonId={authForm.submitButtonId}
        />
      </Form>
    </div>
  )
}

export default AuthForm