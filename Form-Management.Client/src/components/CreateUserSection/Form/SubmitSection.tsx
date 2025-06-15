import { type FC } from 'react'
import { Button, Alert } from 'react-bootstrap/'

interface SubmitSectionProps {
  submitted: boolean
}

const SubmitSection: FC<SubmitSectionProps> = ({ submitted }) => {
  return (
    <div className="d-flex justify-content-between align-items-center">
      <Button variant="primary" type="submit">Submit</Button>
      {submitted && (
        <Alert variant="success" className="px-2 py-1 ml-3 my-auto text-nowrap">
          Form submitted successfully!
        </Alert>
      )}
    </div>
  )
}

export default SubmitSection