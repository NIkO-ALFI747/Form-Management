import {
  useEffect,
  useRef,

  type RefObject,

} from "react";

export const useOutsideClick = <T extends HTMLElement | null>(
  ref: RefObject<T>,
  outsideClick: boolean,
  handleClickInside: (event: MouseEvent) => void,
  handleClickOutside: () => void
) => {

  const listenerAdded = useRef<boolean>(false)

  useEffect(() => {
    const listener = (event: MouseEvent) => {
      if (!ref.current || ref.current.contains(event.target as Node)) {
        handleClickInside(event)
      } else {
        handleClickOutside()
      }
    };
    
    if (!listenerAdded.current) {
      console.log("adding mousedown listener, ref.current: ", ref.current);
      document.addEventListener("mousedown", listener);
      listenerAdded.current = true
    }
    return () => {
      if (listenerAdded.current){
        document.removeEventListener("mousedown", listener);
        listenerAdded.current = false
      }
    };
  }, [ref, outsideClick]);
};
