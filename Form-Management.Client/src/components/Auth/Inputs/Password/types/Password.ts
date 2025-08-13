import type { Dispatch, RefObject, SetStateAction } from "react"
import type { SignUpRequest } from "../../../../../contracts/SignUpRequest"
import type { FormikProps } from "formik"

export interface PasswordProps {
  errorMessage: string | null
  invalidPasswordRef: RefObject<string | null>
  isInvalid: boolean
  setIsInvalid: Dispatch<SetStateAction<boolean>>
  showErrMessages: boolean
  setShowErrMessages: Dispatch<SetStateAction<boolean>>
  formik: FormikProps<SignUpRequest>
  hasBlurHappenedBeforeMouseDownOutside: RefObject<boolean>
  showServerErrMessage: boolean
  setShowServerErrMessage: Dispatch<SetStateAction<boolean>>
}
