export function saveToLocalStorage<T>(key: string, state: T): void {
  try {
    const serializedState = JSON.stringify(state);
    localStorage.setItem(key, serializedState);
  } catch (e) {
    console.warn("Could not save state", e);
  }
}

export function loadFromLocalStorage<T>(key: string): T | undefined {
  try {
    const serializedState = localStorage.getItem(key);
    if (serializedState === null) return undefined;
    return JSON.parse(serializedState);
  } catch (e) {
    console.warn("Could not load state", e);
    return undefined;
  }
}

export function removeFromLocalStorage(key: string): void {
  try {
    localStorage.removeItem(key);
  } catch (e) {
    console.warn("Could not remove state", e);
  }
}