import { type FC } from 'react'
import Form from 'react-bootstrap/Form'
import type { InputGroupProps } from './InputGroupProps.tsx'

const Password: FC<InputGroupProps> = ({ user, setUser }) => {
  return (
    <Form.Group className="mb-3" controlId="formPassword">
      <Form.Label>Password</Form.Label>
      <Form.Control
        type="password"
        placeholder="Enter password"
        required
        value={user?.password ?? ""}
        onChange={(e) => setUser({ ...user!, password: e.target.value })}
      />
    </Form.Group>
  )
}

export default Password