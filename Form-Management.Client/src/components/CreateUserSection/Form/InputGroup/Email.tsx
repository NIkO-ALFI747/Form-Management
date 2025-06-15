import { type FC } from 'react'
import Form from 'react-bootstrap/Form'
import type { InputGroupProps } from './InputGroupProps.tsx'

const Email: FC<InputGroupProps> = ({ user, setUser }) => {
  return (
    <Form.Group className="mb-3" controlId="formEmail">
      <Form.Label>Email address</Form.Label>
      <Form.Control
        type="email"
        placeholder="Enter email"
        required
        value={user?.email ?? ""}
        onChange={(e) => setUser({ ...user!, email: e.target.value })}
      />
      <Form.Text className="text-muted">
        We'll never share your email with anyone else.
      </Form.Text>
    </Form.Group>
  )
}

export default Email