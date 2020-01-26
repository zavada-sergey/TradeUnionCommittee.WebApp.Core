import React from 'react'
import { makeStyles } from '@material-ui/core/styles';
import CssBaseline from '@material-ui/core/CssBaseline';

import Navigation from './Navigation.Component/Navigation'
import Content from './Content.Component/Content'

const useStyles = makeStyles(theme => ({
    markup: {
        display: 'flex',
        backgroundColor: '#ffffff',
        backgroundImage: 'linear-gradient(315deg, #ffffff 0%, #d7e1ec 74%)'
    }
}));

function App() {
    const classes = useStyles();
    return (
        <div className={classes.markup}>
            <CssBaseline />
            <Navigation />
            <Content />
        </div>
    )
}

export default App
