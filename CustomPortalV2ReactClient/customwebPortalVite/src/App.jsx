import React, { Component, Suspense } from 'react'
import { HashRouter, Route, Routes } from 'react-router-dom'
import './scss/style.scss'

import { UrlProvider } from '../src/lib/URLContext.jsx'

const loading = (
  <div className="pt-3 text-center">
    <div className="sk-spinner sk-spinner-pulse"></div>
  </div>
)

// Containers
const DefaultLayout = React.lazy(() => import('./layout/DefaultLayout'))

// Pages
const Login = React.lazy(() => import('./views/pages/login/Login'))
const Register = React.lazy(() => import('./views/pages/register/Register'))
const Page404 = React.lazy(() => import('./views/pages/page404/Page404'))
const Page500 = React.lazy(() => import('./views/pages/page500/Page500'))

class App extends Component {
  constructor(props) {
    super(props)
    this.state = {
      isDarkMode: localStorage.getItem('theme') === 'dark'
    }
  }

  componentDidMount() {
    // Apply saved theme on load
    this.applyTheme(this.state.isDarkMode)
  }

  applyTheme = (isDark) => {
    const htmlElement = document.documentElement
    if (isDark) {
      htmlElement.setAttribute('data-coreui-theme', 'dark')
      localStorage.setItem('theme', 'dark')
    } else {
      htmlElement.removeAttribute('data-coreui-theme')
      localStorage.setItem('theme', 'light')
    }
  }

  toggleDarkMode = () => {
    this.setState(prevState => {
      const newDarkMode = !prevState.isDarkMode
      this.applyTheme(newDarkMode)
      return { isDarkMode: newDarkMode }
    })
  }

  render() {
    return (
      <UrlProvider>
        <HashRouter>
          <Suspense fallback={loading}>
            <Routes>
              <Route exact path="/login" name="Login Page" element={<Login />} />
              <Route exact path="/register" name="Register Page" element={<Register />} />
              <Route exact path="/404" name="Page 404" element={<Page404 />} />
              <Route exact path="/500" name="Page 500" element={<Page500 />} />
              <Route path="*" name="Home" element={<DefaultLayout toggleDarkMode={this.toggleDarkMode} isDarkMode={this.state.isDarkMode} />} />
            </Routes>
          </Suspense>
        </HashRouter>
      </UrlProvider>
    )
  }
}

export default App


