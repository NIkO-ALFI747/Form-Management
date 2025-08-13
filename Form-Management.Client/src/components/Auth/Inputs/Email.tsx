import { type ChangeEventHandler, type Dispatch, type FC, type SetStateAction } from 'react'
import { Form } from 'react-bootstrap/'

interface EmailProps {
  email: string
  onChangeEmail: ChangeEventHandler<HTMLInputElement>
  errorMessage?: string | null
  setErrorMessage?: Dispatch<SetStateAction<string | null>>
}

const Email: FC<EmailProps> = ({ 
  email, 
  onChangeEmail,
  errorMessage
 }) => {
  return (
    <Form.Group className="mb-3" controlId="formEmail">
      <Form.Label>Email address</Form.Label>
      <Form.Control
        type="email"
        name="email"
        placeholder="Enter email"
        required
        value={email}
        onChange={onChangeEmail}
        isInvalid={!!errorMessage}
      />
      <Form.Control.Feedback type="invalid">
        {errorMessage}
      </Form.Control.Feedback>
    </Form.Group>
  )
}

export default Email