import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import { SignUp } from "../pages/Signup";
import { Login } from "../pages/Login";
import { Recipe } from "../pages/Recipe";
import { Home } from "../pages/Home";
import { Profile } from "../pages/Profile";
import { About } from "../pages/About";

export default createBrowserRouter([
  {
    path: "/",
    Component: App,
    children: [
      {
        path: "/",
        Component: Home,
      },
      {
        path: "/signup",
        Component: SignUp,
      },
      {
        path: "/login",
        Component: Login,
      },
      {
        path: "/recipe",
        Component: Recipe,
      },
      {
        path: "/Profile/:id",
        Component: Profile,
      },
      {
        path: "/meal-cards-container",
        Component: Home,
      },
      {
        path: "/About",
        Component: About,
      },
    ],
  },
]);
