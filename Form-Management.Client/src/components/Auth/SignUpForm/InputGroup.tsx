import { type JSX } from 'react'
import type { FormikProps, FormikValues } from 'formik'
import type { UseAuthFormFieldReturn } from '../hooks/useAuthFormField.ts'
import type { UseAuthFormReturn } from '../hooks/useAuthForm.ts'
import AuthInputField from '../Inputs/AuthInputField.tsx'

interface InputGroupProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormFields: UseAuthFormFieldReturn[];
  authForm: UseAuthFormReturn;
}

const InputGroup = <TFormik extends FormikValues>({
  formik,
  authFormFields,
  authForm
}: InputGroupProps<TFormik>): JSX.Element => {
  return (
    <>
      {
        authFormFields.map((authFormField, index) =>
          <AuthInputField
            formik={formik}
            authFormField={authFormField}
            authForm={authForm}
            key={index}
          />
        )
      }
    </>
  )
}

export default InputGroup