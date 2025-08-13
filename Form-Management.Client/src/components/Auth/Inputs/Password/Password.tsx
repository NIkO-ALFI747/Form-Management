/*import {
  useRef,
  type FC,
} from 'react'
import { Form } from 'react-bootstrap/'
import type { PasswordProps } from './types/Password'
import { useFieldInteractionErrorsHandling } from './hooks/useFieldInteractionErrorsHandling/useFieldInteractionErrorsHandling'
import { useSubmissionFieldErrorHandling } from './hooks/useSubmissionFieldErrorHandling'

const Password: FC<PasswordProps> = ({
  formik,
  errorMessage,
  showErrMessages,
  setShowErrMessages,
  isInvalid,
  setIsInvalid,
  invalidPasswordRef,
  hasBlurHappenedBeforeMouseDownOutside,
  showServerErrMessage,
  setShowServerErrMessage
}) => {
  const passwordServerErr = useRef<string>('')
  const prevPasswordServerErr = useRef<string>(passwordServerErr.current)

  useSubmissionFieldErrorHandling({
    formik,
    errorMessage,
    invalidPasswordRef,
    setIsInvalid,
    setShowErrMessages,
    setShowServerErrMessage,
    passwordServerErr
  });

  useFieldInteractionErrorsHandling({
    formik,
    showServerErrMessage,
    invalidPasswordRef,
    setShowServerErrMessage,
    setIsInvalid,
    setShowErrMessages,
    errorMessage,
    passwordServerErr,
    prevPasswordServerErr
  });

  return (
    <Form.Group className="mb-3" controlId="formPassword">
      <Form.Label>Password</Form.Label>
      <Form.Control
        id="PasswordInput"
        type="password"
        name="password"
        placeholder="Enter password"
        required
        value={formik.values.password}
        onChange={formik.handleChange}
        onBlur={(e) => {
          hasBlurHappenedBeforeMouseDownOutside.current = true
          formik.handleBlur(e)
        }}
        isInvalid={isInvalid}
      />
      <Form.Control.Feedback type="invalid">
        {showErrMessages &&
          <>
            {formik.errors.password}
            {showServerErrMessage && !!formik.errors.password && <br />}
            {showServerErrMessage && errorMessage}
          </>
        }
      </Form.Control.Feedback>
    </Form.Group>
  )
}

export default Password*/