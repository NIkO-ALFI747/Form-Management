import { useEffect, useRef, type FC } from 'react'
import { Button, Alert } from 'react-bootstrap/'

interface SubmitSectionProps {
  successfulAuth: boolean
  setSuccessfulAuth: React.Dispatch<React.SetStateAction<boolean>>
  btnTitle: string
  alertTitle: string
  submitCounter: number
  submitButtonId: string
}

const SubmitSection: FC<SubmitSectionProps> = ({
  successfulAuth,
  setSuccessfulAuth,
  btnTitle,
  alertTitle,
  submitCounter,
  submitButtonId
}) => {

  const timeoutRef = useRef<number | null>(null)

  useEffect(() => {
    if (!successfulAuth) {
      if (timeoutRef.current) window.clearTimeout(timeoutRef.current)
      timeoutRef.current = window.setTimeout(() => {
        setSuccessfulAuth(true)
        timeoutRef.current = null
      }, 2000)
    }
  }, [successfulAuth, submitCounter])

  return (
    <div className="d-flex justify-content-between align-items-center">
      <Button
        id={submitButtonId}
        variant="primary"
        type="submit"
        className="text-nowrap"
      >{btnTitle}</Button>
      {!successfulAuth && (
        <Alert variant="danger" className="px-2 py-1 ms-3 my-auto text-wrap text-center">
          {alertTitle}
        </Alert>
      )}
    </div>
  )
}

export default SubmitSection