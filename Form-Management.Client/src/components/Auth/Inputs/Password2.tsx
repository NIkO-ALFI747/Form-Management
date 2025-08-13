import { type ChangeEventHandler, type FC } from 'react'
import { Form } from 'react-bootstrap/'

interface Password2Props {
  password: string
  onChangePassword: ChangeEventHandler<HTMLInputElement>
}

const Password2: FC<Password2Props> = ({ password, onChangePassword }) => {
  return (
    <Form.Group className="mb-3" controlId="formPassword">
      <Form.Label>Password</Form.Label>
      <Form.Control
        type="text"
        name="name"
        placeholder="Enter name"
        required
        value={password}
        onChange={onChangePassword}
      />
    </Form.Group>
  )
}

export default Password2