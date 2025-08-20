import React, { type Dispatch, type SetStateAction } from 'react';
import Form from 'react-bootstrap/Form';

interface RoleSelectorProps {
  selectedRole?: string;
  setSelectedRole?: Dispatch<SetStateAction<string>>
}

const RoleSelector: React.FC<RoleSelectorProps> = ({selectedRole, setSelectedRole}) => {
  const roles = [
    { value: 'User', label: 'User' },
    { value: 'Admin', label: 'Admin' }
  ];

  const handleRoleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedRole?.(event.target.value);
  };

  return (
    <Form.Group className="mb-3" controlId="RoleSelectorId">
      <Form.Label>Select a role</Form.Label>
      <Form.Select
        id="RoleSelectorId"
        name="RoleSelector"
        required
        value={selectedRole}
        onChange={handleRoleChange}
        aria-label="Role Selector"
      >
        {roles.map(role => (
          <option key={role.value} value={role.value}>
            {role.label}
          </option>
        ))}
      </Form.Select>
    </Form.Group>
  );
};

export default RoleSelector;
