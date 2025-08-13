import { type FC } from 'react'
import AuthForm from '../AuthForm.tsx'
import { loginUser as loginUserService } from '../../../services/users.ts'
import { useAuthFormField } from '../hooks/useAuthFormField.ts'
import { useAuthForm } from '../hooks/useAuthForm.ts'
import type { AuthFormProps } from '../SignUpForm/types/AuthForm.ts'
import { useSubmitAuthForm } from '../SignUpForm/hooks/useSubmitSignUpForm/useSubmitAuthForm.ts'
import validationSchema from './loginValidationSchema.ts'

const LoginForm: FC<AuthFormProps> = ({ setIsAuth }) => {
  const password = useAuthFormField({ inputElementId: 'PasswordInput', elementName: 'Password' })
  const email = useAuthFormField({ inputElementId: 'EmailInput', elementName: 'Email' })
  const authFormFields = [email, password]

  const initialAlertTitle = 'Failed to login!';
  const submitButtonTitle = 'Login';
  const submitButtonId = 'LoginButton';

  const authForm = useAuthForm({ initialAlertTitle, submitButtonId })

  const { onSubmitFormik } = useSubmitAuthForm({
    setIsAuth,
    authFormFields,
    authForm,
    authUserService: loginUserService
  });

  const initialValues = {
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
    />
  )
}

export default LoginForm