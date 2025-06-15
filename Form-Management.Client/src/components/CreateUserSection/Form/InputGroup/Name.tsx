import { type FC } from 'react'
import Form from 'react-bootstrap/Form'
import type { InputGroupProps } from './InputGroupProps.tsx'

const Name: FC<InputGroupProps> = ({ user, setUser }) => {
  return (
    <Form.Group className="mb-3" controlId="formName">
      <Form.Label>Name</Form.Label>
      <Form.Control
        type="text"
        placeholder="Enter name"
        required
        value={user?.name ?? ""}
        onChange={(e) => setUser({ ...user!, name: e.target.value })}
      />
    </Form.Group>
  )
}

export default Name