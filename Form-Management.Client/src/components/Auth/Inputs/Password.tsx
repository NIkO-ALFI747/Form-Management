import { useEffect, useRef, useState, type ChangeEventHandler, type Dispatch, type FC, type RefObject, type SetStateAction } from 'react'
import { Form } from 'react-bootstrap/'
import * as formik from 'formik'

interface PasswordProps {
  password: string
  onChangePassword: ChangeEventHandler<HTMLInputElement>
  handleBlur?: {
    (e: React.FocusEvent<any>): void;
    <T = any>(fieldOrEvent: T): T extends string ? (e: any) => void : void;
  }
  setFieldTouched?: (
    field: string,
    isTouched?: boolean,
    shouldValidate?: boolean) =>
    Promise<void | formik.FormikErrors<{
      name: string;
      email: string;
      password: string;
    }>>
  touched?: formik.FormikTouched<{
    name: string;
    email: string;
    password: string;
  }>
  errors?: formik.FormikErrors<{
    name: string;
    email: string;
    password: string;
  }>
  errorMessage: string | null
  setErrorMessage?: Dispatch<SetStateAction<string | null>>
  isSubmitted?: boolean
  setSubmitted?: Dispatch<SetStateAction<boolean>>
  isBlur?: boolean
  setIsBlur?: Dispatch<SetStateAction<boolean>>
  showErrMessages?: boolean
  setShowErrMessages?: Dispatch<SetStateAction<boolean>>
  isSubmittingHandled?: RefObject<boolean>
  isInvalid?: boolean
  setIsInvalid?: Dispatch<SetStateAction<boolean>>
  submitCount?: number
  isValid?: boolean
  lastSubmitCount?: RefObject<number>
  isBlurHandled?: RefObject<boolean>
  invalidPasswordRef?: RefObject<string | null>
  isSubmitting?: boolean
}

const Password: FC<PasswordProps> = ({
  password,
  onChangePassword,
  handleBlur,
  errors,
  errorMessage,
  isBlur,
  setIsBlur,
  showErrMessages,
  setShowErrMessages,
  isInvalid,
  setIsInvalid,
  submitCount,
  isValid,
  lastSubmitCount,
  isBlurHandled,
  invalidPasswordRef,
  isSubmitting
}) => {

  const prevPasswordRef = useRef<string>(password)
  
  const prevPasswordErrRef = useRef<string>(errors?.password)
  const [showServerErrMessage, setShowServerErrMessage] = useState<boolean>(false)
  const [changingServerErrMessageShowing, setChangingServerErrMessageShowing] = useState<boolean>(false)
  const [changingServerErrMessageShowingOnChange, setChangingServerErrMessageShowingOnChange] = useState<boolean>(false)
  const prevServerErrMessageShowingFlagRef = useRef<boolean>(showServerErrMessage)
  const [showServerErrMessageHandled, setShowServerErrMessageHandled] = useState<boolean>(false)
  const checkServerErrMessageShowingRef = useRef<boolean>(false)
  const [checkServerErrMessageShowing, setCheckServerErrMessageShowing] = useState<boolean>(false)
  const checkChangingServerErrMessageShowingOnChange = useRef<boolean>(false)
  const checkChangingServerErrMessageShowing = useRef<boolean>(false)

  useEffect(() => {
    if (!isValid && (submitCount! > lastSubmitCount?.current!)) {
      (lastSubmitCount !== undefined) && (lastSubmitCount.current = submitCount!)
      console.log('setIsInvalid after submitting')
      setIsInvalid?.(!!errors?.password)
      setShowErrMessages?.(true)
      console.log('!!errors?.password: ', !!errors?.password)
    }
  }, [submitCount, isValid])

  useEffect(() => {
    console.log('submitCount', submitCount)
    console.log('lastSubmitCount?.current!', lastSubmitCount?.current!)
    console.log('errorMessage', errorMessage)
    if ((isValid && errorMessage) && (submitCount! > lastSubmitCount?.current!) && !isSubmitting) {
      console.log('setIsInvalid after submitting')
      invalidPasswordRef !== undefined && (invalidPasswordRef.current = password)
      setChangingServerErrMessageShowing(true)
      setShowServerErrMessage(true)
      setShowErrMessages?.(true)
    }
  }, [submitCount, isValid, errorMessage, isSubmitting])

  useEffect(() => {
    if (!checkChangingServerErrMessageShowing.current) {
      checkChangingServerErrMessageShowing.current = true
      return
    }

    if (changingServerErrMessageShowing) {
      console.log('submitCount', submitCount)
      console.log('lastSubmitCount?.current!', lastSubmitCount?.current!)
      console.log('errorMessage', errorMessage)
      if (!!errorMessage && (submitCount! > lastSubmitCount?.current!) && showServerErrMessage) {
        (lastSubmitCount !== undefined) && (lastSubmitCount.current = submitCount!)
        console.log('showServerErrMessage', showServerErrMessage)
        setIsInvalid?.(!!errors?.password || showServerErrMessage)

        prevServerErrMessageShowingFlagRef.current = true
      }
      console.log('showErrMessages', showErrMessages)
      checkChangingServerErrMessageShowing.current = false
      setChangingServerErrMessageShowing(false)
    }

  }, [showServerErrMessage, changingServerErrMessageShowing])


  useEffect(() => {
    if (isBlurHandled !== undefined) {
      if (isBlurHandled.current) {
        if (!isBlur) {
          console.log('setIsInvalid after first isBlurHandled')
          setIsInvalid?.(!!errors?.password || showServerErrMessage)
          setShowErrMessages?.(true)
        }
        isBlurHandled.current = false
      }
    }
  }, [errors?.password])

  useEffect(() => {
    if (password === invalidPasswordRef?.current) {
      setShowServerErrMessage(true)
    } else {
      setShowServerErrMessage(false)
    }
    setChangingServerErrMessageShowingOnChange(true)
  }, [password])

  useEffect(() => {
    if (!checkServerErrMessageShowingRef.current) {
      checkServerErrMessageShowingRef.current = true
      return
    }

    if (checkServerErrMessageShowing) {
      if (prevPasswordErrRef.current !== errors?.password) {
        prevPasswordErrRef.current = errors?.password
      }
      checkServerErrMessageShowingRef.current = false
      setCheckServerErrMessageShowing(false)
    }
  }, [errors?.password, checkServerErrMessageShowing])

  useEffect(() => {




    console.log('prevPasswordRef.current', prevPasswordRef.current)
    console.log('password', password)
    console.log('prevPasswordErrRef.current', prevPasswordErrRef.current)
    console.log('errors?.password: ', errors?.password)


    if (showServerErrMessageHandled) {



      if ((prevPasswordRef.current !== password &&
        prevPasswordErrRef.current !== errors?.password)
      ) {

        console.log('!!errors?.password: ', !!errors?.password)
        console.log('password: ', password)
        setIsInvalid?.(!!errors?.password || showServerErrMessage)
        setShowErrMessages?.(!!errors?.password || showServerErrMessage)
        prevPasswordRef.current = password
        prevPasswordErrRef.current = errors?.password

      }
      setShowServerErrMessageHandled(false)
    }
  }, [password, errors?.password, showServerErrMessageHandled])

  useEffect(() => {
    console.log('prevPasswordRef.current', prevPasswordRef.current)
    console.log('password', password)
    console.log('prevServerErrMessageShowingFlagRef.current', prevServerErrMessageShowingFlagRef.current)
    console.log('showServerErrMessage', showServerErrMessage)
    if (!checkChangingServerErrMessageShowingOnChange.current) {
      checkChangingServerErrMessageShowingOnChange.current = true
      return
    }

    if (changingServerErrMessageShowingOnChange) {


      if (prevPasswordRef.current !== password) {

        if (
          prevServerErrMessageShowingFlagRef.current !== showServerErrMessage
        ) {
          console.log('password: ', password)
          console.log('setting err message showing')
          setIsInvalid?.(!!errors?.password || showServerErrMessage)
          setShowErrMessages?.(!!errors?.password || showServerErrMessage)
          prevPasswordRef.current = password
          prevServerErrMessageShowingFlagRef.current = showServerErrMessage
          setShowServerErrMessageHandled(false)
          setCheckServerErrMessageShowing(true)
        } else {
          setShowServerErrMessageHandled(true)
        }
      }
      checkChangingServerErrMessageShowingOnChange.current = false
      setChangingServerErrMessageShowingOnChange(false)
    }

  }, [showServerErrMessage, changingServerErrMessageShowingOnChange])

  /*useEffect(() => {
    if (isBlurHandled.current) {
      setShowErrMessages?.(true)
      setIsInvalid?.(!!errors?.password || !!errorMessage)
      isBlurHandled.current = false
    }
  }, [isBlur])*/

  /*
  useEffect(() => {
    isBlur
      ? setIsInvalid(!!errors?.password || !!errorMessage)
      : setIsInvalid(showErrMessages! && (!!errors?.password || !!errorMessage))
  }, [showErrMessages])
  */

  /*useEffect(() => {
    isSubmitted && setSubmitted?.(false)
    setShowErrMessages?.(!isBlur)
    errorMessage && (setErrorMessage?.(null))
  }, [password])*/

  return (
    <Form.Group className="mb-3" controlId="formPassword">
      <Form.Label>Password</Form.Label>
      <Form.Control
        id="PasswordInput"
        type="password"
        name="password"
        placeholder="Enter password"
        required
        value={password}
        onChange={onChangePassword}
        onFocus={() => {
          setIsBlur?.(false)
        }}
        onBlur={(e) => {
          !(errors?.password) && (isBlurHandled !== undefined) && (isBlurHandled.current = true)
          setIsBlur?.(true)
          handleBlur?.(e)
        }}
        isInvalid={isInvalid}
      />
      <Form.Control.Feedback type="invalid">
        {showErrMessages &&
          <>
            {errors?.password}
            {showServerErrMessage && !!errors?.password && <br />}
            {showServerErrMessage && errorMessage}
          </>
        }
      </Form.Control.Feedback>
    </Form.Group>
  )
}

export default Password