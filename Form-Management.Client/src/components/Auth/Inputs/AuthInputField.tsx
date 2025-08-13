import { useRef } from 'react'
import { Form } from 'react-bootstrap/'
import type { AuthInputFieldProps } from './types'
import type { FormikValues } from 'formik'
import { useSubmissionFieldErrorHandling } from './Password/hooks/useSubmissionFieldErrorHandling'
import { useFieldInteractionErrorsHandling } from './Password/hooks/useFieldInteractionErrorsHandling/useFieldInteractionErrorsHandling'

const AuthInputField = <TFormik extends FormikValues>({
  formik,
  authFormField,
  authForm,
}: AuthInputFieldProps<TFormik>) => {
  const currentFieldServerErr = useRef<string>('')
  const prevFieldServerErr = useRef<string>(currentFieldServerErr.current)

  useSubmissionFieldErrorHandling({
    formik,
    authFormField,
    currentFieldServerErr
  });

  useFieldInteractionErrorsHandling({
    formik,
    authFormField,
    currentFieldServerErr,
    prevFieldServerErr
  });

  return (
    <Form.Group className="mb-3" controlId={`form${authFormField.elementName}`}>
      <Form.Label>{authFormField.elementName}</Form.Label>
      <Form.Control
        id={authFormField.inputElementId}
        type={authFormField.elementName.toLowerCase()}
        name={authFormField.elementName.toLowerCase()}
        placeholder={`Enter ${authFormField.elementName.toLowerCase()}`}
        required
        value={formik.values[authFormField.elementName.toLowerCase()]}
        onChange={formik.handleChange}
        onBlur={(e) => {
          authForm.hasBlurHappenedBeforeMouseDownOutside.current = true
          formik.handleBlur(e)
        }}
        isInvalid={authFormField.isInvalid}
      />
      <Form.Control.Feedback type="invalid">
        {authFormField.showErrMessages &&
          <>
            {formik.errors[authFormField.elementName.toLowerCase()]}
            {
              authFormField.showServerErrMessage &&
              !!formik.errors[authFormField.elementName.toLowerCase()] && <br />
            }
            {authFormField.showServerErrMessage && authFormField.serverErrorMessage}
          </>
        }
      </Form.Control.Feedback>
    </Form.Group>
  )
}

export default AuthInputField