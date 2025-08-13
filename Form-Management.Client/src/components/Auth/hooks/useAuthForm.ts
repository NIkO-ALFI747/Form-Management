import {
  useRef,
  useState,
  type Dispatch,
  type SetStateAction,
  type RefObject,
} from "react";

interface UseAuthFormProps {
  initialAlertTitle: string;
  submitButtonId: string;
}

export interface UseAuthFormReturn {
  successfulAuth: boolean;
  setSuccessfulAuth: Dispatch<SetStateAction<boolean>>;
  alertTitle: string;
  setAlertTitle: Dispatch<SetStateAction<string>>;
  hasBlurHappenedBeforeMouseDownOutside: RefObject<boolean>;
  submitButtonId: string;
}

export const useAuthForm = ({
  initialAlertTitle,
  submitButtonId,
}: UseAuthFormProps): UseAuthFormReturn => {
  const [successfulAuth, setSuccessfulAuth] = useState<boolean>(true);
  const [alertTitle, setAlertTitle] = useState<string>(initialAlertTitle);
  const hasBlurHappenedBeforeMouseDownOutside = useRef<boolean>(false);
  const SubmitButtonId = submitButtonId;

  return {
    successfulAuth,
    setSuccessfulAuth,
    alertTitle,
    setAlertTitle,
    hasBlurHappenedBeforeMouseDownOutside,
    submitButtonId: SubmitButtonId,
  };
};
