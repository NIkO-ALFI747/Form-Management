import { type FC, useState, useEffect, useRef } from 'react'
import { Form } from 'react-bootstrap/'
import type { User } from '../../types/User.tsx'

interface HeadCheckboxProps {
  users: User[]
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  selectedCount: number
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
}

const HeadCheckbox: FC<HeadCheckboxProps> = ({ users, setUsers, selectedCount, setSelectedCount }) => {

  const [areAllSelected, setAreAllSelected] = useState<boolean>(false)

  const [areSomeSelected, setAreSomeSelected] = useState<boolean>(false)

  const headerCheckboxRef = useRef<HTMLInputElement>(null)

  const onHeaderCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUsers(prevUsers =>
      prevUsers.map(prU => ({ ...prU, isSelected: e.target.checked }))
    )
    setSelectedCount(e.target.checked ? users.length : 0)
  }

  useEffect(() => {
    const areAllSelected = (selectedCount === users.length) && (selectedCount > 0)
    setAreAllSelected(areAllSelected)
    setAreSomeSelected((selectedCount > 0) && !areAllSelected)
  }, [selectedCount])

  useEffect(() => {
    if (headerCheckboxRef.current) {
      headerCheckboxRef.current.indeterminate = areSomeSelected
    }
  }, [areSomeSelected])

  return (
    <Form.Check>
      <Form.Check.Input
        type="checkbox"
        checked={areAllSelected}
        ref={headerCheckboxRef}
        onChange={onHeaderCheckboxChange}
        id="header-checkbox"
      />
    </Form.Check>
  )
}

export default HeadCheckbox