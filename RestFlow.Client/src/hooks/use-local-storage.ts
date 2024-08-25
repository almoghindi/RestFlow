const loadFromLocalStorage = (key: string) => {
  try {
    const serializedState = localStorage.getItem(key);
    return serializedState ? JSON.parse(serializedState) : undefined;
  } catch (err) {
    console.error(err);
    return undefined;
  }
};

const saveToLocalStorage = (key: string, state: any) => {
  try {
    const serializedState = JSON.stringify(state);
    localStorage.setItem(key, serializedState);
  } catch (err) {
    console.error(err);
  }
};

export { loadFromLocalStorage, saveToLocalStorage };
