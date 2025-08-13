import { type ChangeEventHandler, type FC } from 'react'
import { Form } from 'react-bootstrap/'

interface NameProps {
  name: string
  onChangeName: ChangeEventHandler<HTMLInputElement>
}

const Name: FC<NameProps> = ({ name, onChangeName }) => {
  return (
    <Form.Group className="mb-3" controlId="formName">
      <Form.Label>Name</Form.Label>
      <Form.Control
        type="text"
        name="name"
        placeholder="Enter name"
        required
        value={name}
        onChange={onChangeName}
      />
    </Form.Group>
  )
}

export default Name