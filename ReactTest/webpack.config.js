const path = require('path');
const CopyPlugin = require("copy-webpack-plugin");

module.exports = {
    mode: 'development',
    entry: {
        "main": [
            './index.tsx'
        ]
    },
    resolve: {
        alias: {
            components: path.resolve(__dirname, './components'),
        },
        extensions: ['.js', '.jsx', '.ts', '.tsx'],
    },
    output: {
        path: path.resolve(__dirname, './deploy/dist'),
        filename: '[name].js',
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-react']
                    }
                }
            },
            {
                test: /\.css$/i,
                use: [
                    {
                        loader: 'css-loader'
                    }
                ]
            },
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
        ]
    },
    plugins: [
        new CopyPlugin({
            patterns: [
                {
                    from: 'css',
                    to: './'
                }
            ]
        })
    ]
}