import { useCallback, useEffect } from "react";
import type { UseMouseDownRefObjectProps } from "./types";

export const useMouseDownRefObject = <T extends HTMLElement | null>({
  mouseDownRefObject,
  handleMouseDownInsideRefObject,
  handleMouseDownOutsideRefObject,
}: UseMouseDownRefObjectProps<T>) => {
  const handleMouseDown = useCallback(
    (event: MouseEvent) => {
      if (
        !mouseDownRefObject.current ||
        mouseDownRefObject.current.contains(event.target as Node)
      ) {
        handleMouseDownInsideRefObject();
      } else {
        event.stopPropagation();
        handleMouseDownOutsideRefObject();
      }
    },
    [
      mouseDownRefObject,
      handleMouseDownInsideRefObject,
      handleMouseDownOutsideRefObject,
    ]
  );

  useEffect(() => {
    document.addEventListener("mousedown", handleMouseDown);
    return () => {
      document.removeEventListener("mousedown", handleMouseDown);
    };
  }, [handleMouseDown]);
};
