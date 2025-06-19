import { type FC } from 'react'
import Form from 'react-bootstrap/Form'

interface PasswordProps {
  password: string | undefined
  onChangePassword: (password: string) => void
}

const Password: FC<PasswordProps> = ({ password, onChangePassword }) => {
  return (
    <Form.Group className="mb-3" controlId="formPassword">
      <Form.Label>Password</Form.Label>
      <Form.Control
        type="password"
        placeholder="Enter password"
        required
        value={password ?? ""}
        onChange={(e) => onChangePassword(e.target.value)}
      />
    </Form.Group>
  )
}

export default Password