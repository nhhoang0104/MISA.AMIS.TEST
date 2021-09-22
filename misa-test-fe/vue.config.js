const path = require("path");

module.exports = {
  // runtimeCompiler: true,
  // css: {
  //   requireModuleExtension: true,
  // },
  configureWebpack: {
    resolve: {
      alias: {
        "@": path.join(__dirname, "src/"),
      },
    },
    devtool: "source-map",
    // plugins: [new MyAwesomeWebpackPlugin()],
  },
};
