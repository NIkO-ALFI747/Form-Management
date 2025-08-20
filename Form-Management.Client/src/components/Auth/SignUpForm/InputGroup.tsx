import { type Dispatch, type JSX, type SetStateAction } from 'react'
import type { FormikProps, FormikValues } from 'formik'
import type { UseAuthFormFieldReturn } from '../hooks/useAuthFormField.ts'
import type { UseAuthFormReturn } from '../hooks/useAuthForm.ts'
import AuthInputField from '../Inputs/AuthInputField.tsx'
import RoleSelector from '../Inputs/RoleSelector.tsx'

interface InputGroupProps<TFormik extends FormikValues> {
  formik: FormikProps<TFormik>;
  authFormFields: UseAuthFormFieldReturn[];
  authForm: UseAuthFormReturn;
  isRoleSet?: boolean;
  selectedRole?: string;
  setSelectedRole?: Dispatch<SetStateAction<string>>
}

const InputGroup = <TFormik extends FormikValues>({
  formik,
  authFormFields,
  authForm,
  isRoleSet = false,
  selectedRole,
  setSelectedRole
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
      {isRoleSet && 
      <RoleSelector 
        selectedRole={selectedRole}
        setSelectedRole={setSelectedRole}
      />}
    </>
  )
}

export default InputGroup