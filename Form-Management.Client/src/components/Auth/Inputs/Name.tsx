import { type FC } from 'react'
import Form from 'react-bootstrap/Form'

interface NameProps {
  name: string | undefined
  onChangeName: (name: string) => void
}

const Name: FC<NameProps> = ({ name, onChangeName }) => {
  return (
    <Form.Group className="mb-3" controlId="formName">
      <Form.Label>Name</Form.Label>
      <Form.Control
        type="text"
        placeholder="Enter name"
        required
        value={name ?? ""}
        onChange={(e) => onChangeName(e.target.value)}
      />
    </Form.Group>
  )
}

export default Name