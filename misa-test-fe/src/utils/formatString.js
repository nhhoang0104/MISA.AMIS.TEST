String.prototype.format = function() {
  let tmp = this;
  for (let k in arguments) {
    tmp = tmp.replace("{" + k + "}", arguments[k]);
  }
  return tmp;
};
