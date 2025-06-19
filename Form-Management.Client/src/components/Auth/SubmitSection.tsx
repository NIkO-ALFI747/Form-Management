import { useEffect, useRef, type FC } from 'react'
import { Button, Alert } from 'react-bootstrap/'

interface SubmitSectionProps {
  successfulAuth: boolean
  setSuccessfulAuth: React.Dispatch<React.SetStateAction<boolean>>
  btnTitle: string
  alertTitle: string
}

const SubmitSection: FC<SubmitSectionProps> = ({
  successfulAuth, setSuccessfulAuth, btnTitle, alertTitle
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
  }, [successfulAuth])

  return (
    <div className="d-flex justify-content-between align-items-center">
      <Button variant="primary" type="submit">{btnTitle}</Button>
      {!successfulAuth && (
        <Alert variant="danger" className="px-2 py-1 ml-3 my-auto text-nowrap">
          {alertTitle}
        </Alert>
      )}
    </div>
  )
}

export default SubmitSection