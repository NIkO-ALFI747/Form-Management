import { useEffect, useRef, type FC } from 'react'
import { Button, Alert } from 'react-bootstrap/'

interface SubmitSectionProps {
  submitted: boolean
  setSubmitted: React.Dispatch<React.SetStateAction<boolean>>
}

const SubmitSection: FC<SubmitSectionProps> = ({ submitted, setSubmitted }) => {

  const timeoutRef = useRef<number | null>(null)

  useEffect(() => {
    if (submitted) {
      if (timeoutRef.current) window.clearTimeout(timeoutRef.current)
      timeoutRef.current = window.setTimeout(() => {
        setSubmitted(false)
        timeoutRef.current = null
      }, 2000)
    }
  }, [submitted])

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