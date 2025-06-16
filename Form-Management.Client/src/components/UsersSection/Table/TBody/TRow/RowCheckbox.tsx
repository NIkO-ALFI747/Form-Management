import { type FC } from 'react'
import Form from 'react-bootstrap/Form'
import type { User } from '../../../types/User.tsx'

interface RowCheckboxProps {
  userIndex: number
  user: User
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
}

const RowCheckbox: FC<RowCheckboxProps> = ({ userIndex, user, setUsers, setSelectedCount }) => {
  const onRowCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>, index: number) => {
    setUsers(prevUsers => {
      const users = [...prevUsers]
      users[index] = { ...users[index], isSelected: e.target.checked }
      return users
    })
    setSelectedCount(prevCount => e.target.checked ? prevCount + 1 : prevCount - 1)
  }

  return (
    <Form.Check>
      <Form.Check.Input
        type="checkbox"
        checked={user.isSelected}
        onChange={(e) => onRowCheckboxChange(e, userIndex)}
        id={`checkbox-${user.id}`}
      />
    </Form.Check>
  )
}

export default RowCheckbox