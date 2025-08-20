import { type FC } from 'react'
import type { AuthFormProps } from './types/AuthForm.ts'
import { useSubmitAuthForm } from './hooks/useSubmitSignUpForm/useSubmitAuthForm.ts'
import AuthForm from '../AuthForm.tsx'
import { signUpUser as signUpUserService } from '../../../services/users.ts'
import { useAuthFormField } from '../hooks/useAuthFormField.ts'
import { useAuthForm } from '../hooks/useAuthForm.ts'
import validationSchema from './validationSchema.ts'

const SignUpForm: FC<AuthFormProps> = ({ setIsAuth }) => {
  const password = useAuthFormField({ inputElementId: 'PasswordInput', elementName: 'Password' });
  const email = useAuthFormField({ inputElementId: 'EmailInput', elementName: 'Email' });
  const name = useAuthFormField({ inputElementId: 'NameInput', elementName: 'Name' });
  const authFormFields = [name, email, password];

  const initialAlertTitle = 'Failed to sign up!';
  const submitButtonTitle = 'Sign Up';
  const submitButtonId = 'SignUpButton';

  const authForm = useAuthForm({ initialAlertTitle, submitButtonId })

  const { onSubmitFormik } = useSubmitAuthForm({
    setIsAuth,
    authFormFields,
    authForm,
    authUserService: signUpUserService
  });

  const initialValues = {
    name: "",
    email: "",
    password: "",
  }

  return (
    <AuthForm
      onSubmitFormik={onSubmitFormik}
      initialValues={initialValues}
      authForm={authForm}
      authFormFields={authFormFields}
      submitButtonTitle={submitButtonTitle}
      validationSchema={validationSchema}
      isRoleSet={true}
    />
  )
}

export default SignUpForm