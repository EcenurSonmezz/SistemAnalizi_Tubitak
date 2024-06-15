  /* eslint-disable react/prop-types */
  /* eslint-disable react-hooks/rules-of-hooks */

  import { createContext, useState } from "react";
  import { loadLoginState, storeLoginState } from "./storage";

  export const LoginContext = createContext();

  export function LoginationContext({ children }) {
    const [login, setLogin] = useState(loadLoginState());

    const onLoginSuccess = (data) => {
      setLogin(data);
      storeLoginState(data);
    };

    const onLogotSuccess = () => {
      const defaultState = { id: "0" };
      setLogin(defaultState);
      storeLoginState(defaultState);
    };

    const setType = (type) => {
      setLogin((prevState) => {
        const updatedState = { ...prevState, type };
        storeLoginState(updatedState);
        return updatedState;
      });
    };

    return (
      <LoginContext.Provider
        value={{
          ...login,
          onLoginSuccess,
          onLogotSuccess,
          setType,
        }}
      >
        {children}
      </LoginContext.Provider>
    );
  }
