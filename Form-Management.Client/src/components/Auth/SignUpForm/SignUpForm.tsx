import { useEffect, useRef, useState, type FC, type FormEvent, type MouseEvent as reactMouseEvent } from 'react'
import { AxiosError } from 'axios'
import { Form } from 'react-bootstrap/'
import InputGroup from './InputGroup.tsx'
import SubmitSection from '../SubmitSection.tsx'
import type { SignUpRequest } from '../../../contracts/SignUpRequest.tsx'
import { signUpUser as signUpUserService } from '../../../services/users.ts'
import * as formik from 'formik'
import validationSchema from './validationSchema.ts'
import { useOutsideClick } from '../../../hooks/useOutsideClick.ts'

interface SignUpFormProps {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

interface ValidationErrors {
  Password: string[] | undefined
  Email: string[] | undefined
}

interface Error {
  detail: string
  errors: ValidationErrors | null
}


interface OnSubmit {
  setSubmitting: (isSubmitting: boolean) => void
  setFieldTouched?: (field: string, isTouched?: boolean, shouldValidate?: boolean) => Promise<void | formik.FormikErrors<{
    name: string;
    email: string;
    password: string;
  }>>
  resetForm: (nextState?: Partial<formik.FormikState<{
    name: string;
    email: string;
    password: string;
  }>> | undefined) => void
}

const SignUpForm: FC<SignUpFormProps> = ({ setIsAuth }) => {

  const [successfulSignUp, setSuccessfulSignUp] = useState<boolean>(true)

  const [alertTitle, setAlertTitle] = useState<string>('Failed to sign up!')

  const [passwordErrorMessage, setPasswordErrorMessage] = useState<string | null>(null)

  const [emailErrorMessage, setEmailErrorMessage] = useState<string | null>(null)

  const [isSubmitted, setSubmitted] = useState<boolean>(false)

  const [isBlur, setIsBlur] = useState<boolean>(true)

  const [showErrMessages, setShowErrMessages] = useState<boolean>(false)

  const [isInvalid, setIsInvalid] = useState<boolean>(false)

  const isSubmittingHandled = useRef(false)

  const isBlurHandled = useRef(false)

  const invalidPasswordRef = useRef<string | null>(null)

  const { Formik } = formik

  const signUpUser = async (user: SignUpRequest, {
    setSubmitting, resetForm
  }: OnSubmit) => {
    try {
      await signUpUserService(user)
      setIsAuth(true)
      setIsBlur(true) // other custom resetting
      resetForm()
      /*isSubmittingHandled.current = false*/
    } catch (e) {
      const errorMessages = (e as AxiosError).response?.data as Error | undefined
      if (errorMessages) {
        const validationErrorMessages = errorMessages.errors as ValidationErrors | undefined
        if (validationErrorMessages) {
          if (validationErrorMessages.Password !== undefined) {
            validationErrorMessages.Password.every(el => el === '')
              ? setPasswordErrorMessage("Please provide a valid password.")
              : setPasswordErrorMessage(validationErrorMessages.Password.join('\n'))
          }
          if (validationErrorMessages.Email !== undefined) {
            validationErrorMessages.Email.every(el => el === '')
              ? setEmailErrorMessage("Please provide a valid email.")
              : setEmailErrorMessage(validationErrorMessages.Email.join('\n'))
          }
        } else {
          const errorDetailMessage = errorMessages.detail as string
          (errorDetailMessage !== '') && setAlertTitle(errorDetailMessage)
        }
        setSuccessfulSignUp(false)
      }
    } finally {
      setSubmitting(false)
    }
  }

  const [values, setValues] = useState<SignUpRequest | null>(null)
  const [onSubmitMethods, setOnSubmitMethods] = useState<OnSubmit | null>(null)

  useEffect(() => {
    if (values && onSubmitMethods && !passwordErrorMessage && !emailErrorMessage) {
      signUpUser(values, onSubmitMethods)
      setValues(null)
      setOnSubmitMethods(null)
    }
  }, [values, onSubmitMethods, passwordErrorMessage, emailErrorMessage])

  const onSubmit = async (user: SignUpRequest, {
    setSubmitting, setFieldTouched, resetForm
  }: OnSubmit) => {
    setValues(user)
    setOnSubmitMethods({
      setSubmitting, setFieldTouched, resetForm
    })
    if (invalidPasswordRef.current === user.password) {
      setSubmitting(false)
    } else {
      invalidPasswordRef.current = null
      setPasswordErrorMessage(null)
      setEmailErrorMessage(null)
    }
  }

  const handleMouseDown = (event: reactMouseEvent<HTMLDivElement>, errors: formik.FormikErrors<{
    name: string;
    email: string;
    password: string;
  }>,
    values: {
      name: string;
      email: string;
      password: string;
    },
    touched: formik.FormikTouched<{
      name: string;
      email: string;
      password: string;
    }>
  ) => {
    const clickedElement = event.target as HTMLElement
    console.log('MouseEvent')
    if (!(clickedElement instanceof HTMLButtonElement && clickedElement.id === "SignUpButton")) {
      console.log('MouseEvent outside SignUpButton')
      /*isSubmittingHandled.current = false*/
      if (
        values.name || values.email || values.password
      ) {
        console.log('setIsInvalid after values.name || values.email || values.password')
        console.log('!!errors?.password: ', !!errors?.password)
        setIsInvalid(!!errors?.password || !!passwordErrorMessage)
      } else {
        if (clickedElement instanceof HTMLInputElement && clickedElement.id === "PasswordInput") {
          console.log('setIsInvalid after clickedElement.id === "PasswordInput"')
          setIsInvalid(touched.password! && (!!errors?.password || !!passwordErrorMessage))
        }
        else {
          console.log('setIsInvalid after clickedElement.id !== "PasswordInput"')
          setIsInvalid(false)
        }
      }
      if (clickedElement instanceof HTMLInputElement && clickedElement.id === "PasswordInput") {
        console.log('setShowErrMessages after clickedElement.id === "PasswordInput"')
        //setShowErrMessages(!isBlur) // isBlur may not be ready on time as this event happens with mousedown
        setShowErrMessages(true)
      } else {
        console.log('setShowErrMessages after clickedElement.id !== "PasswordInput"')
        setShowErrMessages(false)
      }
    }
  }



  return (
    <Formik
      validationSchema={validationSchema}
      onSubmit={(values, { setSubmitting, setFieldTouched, resetForm }) => onSubmit(values, { setSubmitting, setFieldTouched, resetForm })}
      initialValues={{
        name: '',
        email: '',
        password: ''
      }}
    >
      {({ handleSubmit, handleChange, handleBlur, setFieldTouched, values,
        touched, errors, submitCount, isValid, dirty, isSubmitting }) => {
        const lastSubmitCount = useRef(submitCount)
        const outsideClickRef = useRef<HTMLDivElement>(null)
        const [outsideClick, setOutsideClick] = useState<boolean>(true)

        const handleClickInside = (event: MouseEvent) => {
          console.log("click inside, ref.current: ", outsideClickRef.current);
          console.log("outsideClick: ", outsideClick);
          if (outsideClick) {
            setOutsideClick(false);
            const clickedElement = event.target as HTMLElement
            if (clickedElement instanceof HTMLInputElement && clickedElement.id === "PasswordInput") {
              if (values.password) {
                console.log("handleClickInside, touched.password: ",
                  touched.password, "errors.password: ", errors.password);
                //setFieldTouched('password', true)
              }
              console.log("handleClickInside, touched.password: ",
                touched.password, "errors.password: ", errors.password);
            }
          }
        }
        const handleClickOutside = () => {
          console.log("click outside, ref.current: ", outsideClickRef.current);
          console.log("outsideClick: ", outsideClick);
          !outsideClick && setOutsideClick(true);
        }

        useOutsideClick(outsideClickRef, outsideClick,
          handleClickInside, handleClickOutside
        )

        useEffect(() => {
          if (outsideClick) {
            /*isSubmittingHandled.current = false*/
            !isBlur && setIsBlur(true)
            setIsInvalid(false)
            setShowErrMessages(false)
            !isSubmitted && setSubmitted(false)
          }

          console.log('useEffect outsideClick: ', outsideClick)
          /*if (outsideClick && isBlur && !isInvalid &&
            !showErrMessages && !isSubmittingHandled.current
            && !isSubmitted) {
            console.log('setIsInvalid after resetForm')
            //setFieldTouched('password', false)
          }*/
        }, [outsideClick /*, isBlur, isInvalid, showErrMessages, isSubmitted*/]);

        useEffect(() => {
          if (outsideClick && isBlur && !isInvalid &&
            !showErrMessages /*&& !isSubmittingHandled.current*/
            && !isSubmitted) {
            console.log('setIsInvalid after resetForm')
            setFieldTouched('password', false)
          }
        }, [outsideClick, isBlur, isInvalid, showErrMessages, isSubmitted]);

        useEffect(() => {
          console.log('touched.password: ', touched.password)
        }, [touched.password]);

        useEffect(() => {
          console.log('useEffect outsideClick, dirty: ', outsideClick)
          if (!dirty && outsideClick) {
            console.log('setIsInvalid after reset own states')
            //lastSubmitCount.current = submitCount
          }
        }, [outsideClick, dirty]);

        return (
          <div
            onMouseDown={(e) => handleMouseDown(e, errors, values, touched)}
            ref={outsideClickRef}
          >
            <Form noValidate onSubmit={
              (e: FormEvent<HTMLFormElement>) => {
                //!(errors?.password) && (isSubmittingHandled.current = true)
                console.log('SubmitEvent')
                handleSubmit(e)
              }
            }>
              <InputGroup
                passwordErrorMessage={passwordErrorMessage}
                setPasswordErrorMessage={setPasswordErrorMessage}
                emailErrorMessage={emailErrorMessage}
                setEmailErrorMessage={setEmailErrorMessage}
                handleChange={handleChange}
                handleBlur={handleBlur}
                touched={touched}
                setFieldTouched={setFieldTouched}
                values={values}
                errors={errors}
                isSubmitted={isSubmitted}
                setSubmitted={setSubmitted}
                isBlur={isBlur}
                setIsBlur={setIsBlur}
                showErrMessages={showErrMessages}
                setShowErrMessages={setShowErrMessages}
                isSubmittingHandled={isSubmittingHandled}
                isInvalid={isInvalid}
                setIsInvalid={setIsInvalid}
                submitCount={submitCount}
                isValid={isValid}
                lastSubmitCount={lastSubmitCount}
                isBlurHandled={isBlurHandled}
                invalidPasswordRef={invalidPasswordRef}
                isSubmitting={isSubmitting}
              />
              <SubmitSection successfulAuth={successfulSignUp}
                setSuccessfulAuth={setSuccessfulSignUp}
                btnTitle='Sign Up'
                alertTitle={alertTitle}
                submitCounter={submitCount}
              />
            </Form>
          </div>
        )

      }}
    </Formik>
  )
}

export default SignUpForm