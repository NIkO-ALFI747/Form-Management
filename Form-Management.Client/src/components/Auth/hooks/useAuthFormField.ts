import {
  useRef,
  useState,
  type Dispatch,
  type SetStateAction,
  type RefObject,
} from "react";

interface UseAuthFormFieldProps {
  inputElementId: string;
  elementName: string;
}

export interface UseAuthFormFieldReturn {
  serverErrorMessage: string | null;
  setServerErrorMessage: Dispatch<SetStateAction<string | null>>;
  showErrMessages: boolean;
  setShowErrMessages: Dispatch<SetStateAction<boolean>>;
  isInvalid: boolean;
  setIsInvalid: Dispatch<SetStateAction<boolean>>;
  invalidFieldRef: RefObject<string | null>;
  showServerErrMessage: boolean;
  setShowServerErrMessage: Dispatch<SetStateAction<boolean>>;
  inputElementId: string;
  elementName: string;
}

export const useAuthFormField = ({inputElementId, elementName} : UseAuthFormFieldProps): UseAuthFormFieldReturn => {
  const [serverErrorMessage, setServerErrorMessage] = useState<string | null>(
    null
  );
  const [showErrMessages, setShowErrMessages] = useState<boolean>(false);
  const [isInvalid, setIsInvalid] = useState<boolean>(false);
  const invalidFieldRef = useRef<string | null>(null);
  const [showServerErrMessage, setShowServerErrMessage] =
    useState<boolean>(false);
  const InputElementId = inputElementId;
  const ElementName = elementName;

  return {
    serverErrorMessage,
    setServerErrorMessage,
    showErrMessages,
    setShowErrMessages,
    isInvalid,
    setIsInvalid,
    invalidFieldRef,
    showServerErrMessage,
    setShowServerErrMessage,
    inputElementId: InputElementId,
    elementName: ElementName
  };
};
