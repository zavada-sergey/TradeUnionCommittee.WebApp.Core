import React from 'react'
import { Router } from 'react-router-dom'
import { createBrowserHistory } from 'history'
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import store from './store'
import RootContainer from './containers/RootContainer'
import * as serviceWorker from './utils/serviceWorker'
import './styles/globalStyles.css'

export const history = createBrowserHistory()

render(
  <Provider store={store}>
    <Router history={history}>
      <RootContainer />
    </Router>
  </Provider>,
  document.getElementById('root'),
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister()
