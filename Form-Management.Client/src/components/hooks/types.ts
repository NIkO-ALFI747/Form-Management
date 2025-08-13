import type { RefObject } from "react";

export interface UseMouseDownRefObjectProps<T extends HTMLElement | null> {
  mouseDownRefObject: RefObject<T>;
  handleMouseDownInsideRefObject: () => void;
  handleMouseDownOutsideRefObject: () => void;
}
