import { type FC } from 'react'
import Form from 'react-bootstrap/Form'

interface EmailProps {
  email: string | undefined
  onChangeEmail: (email: string) => void
}

const Email: FC<EmailProps> = ({ email, onChangeEmail }) => {
  return (
    <Form.Group className="mb-3" controlId="formEmail">
      <Form.Label>Email address</Form.Label>
      <Form.Control
        type="email"
        placeholder="Enter email"
        required
        value={email ?? ""}
        onChange={(e) => onChangeEmail(e.target.value)}
      />
    </Form.Group>
  )
}

export default Email