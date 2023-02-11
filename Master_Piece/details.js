window.Modernizr = function (e, t, n) {
    function r(e) {
        m.cssText = e
    }
    function a(e, t) {
        return typeof e === t
    }
    var o, c, i = "2.6.2", l = {}, u = !0, s = t.documentElement, d = "modernizr", f = t.createElement(d), m = f.style, h = ({}.toString,
        {}), p = [], v = p.slice, g = {}.hasOwnProperty;
    for (var y in c = a(g, "undefined") || a(g.call, "undefined") ? function (e, t) {
        return t in e && a(e.constructor.prototype[t], "undefined")
    }
        : function (e, t) {
            return g.call(e, t)
        }
        ,
        Function.prototype.bind || (Function.prototype.bind = function (e) {
            var t = this;
            if ("function" != typeof t)
                throw new TypeError;
            var n = v.call(arguments, 1)
                , r = function () {
                    if (this instanceof r) {
                        var a = function () { };
                        a.prototype = t.prototype;
                        var o = new a
                            , c = t.apply(o, n.concat(v.call(arguments)));
                        return Object(c) === c ? c : o
                    }
                    return t.apply(e, n.concat(v.call(arguments)))
                };
            return r
        }
        ),
        h)
        c(h, y) && (o = y.toLowerCase(),
            l[o] = h[y](),
            p.push((l[o] ? "" : "no-") + o));
    return l.addTest = function (e, t) {
        if ("object" == typeof e)
            for (var r in e)
                c(e, r) && l.addTest(r, e[r]);
        else {
            if (e = e.toLowerCase(),
                l[e] !== n)
                return l;
            t = "function" == typeof t ? t() : t,
                void 0 !== u && u && (s.className += " " + (t ? "" : "no-") + e),
                l[e] = t
        }
        return l
    }
        ,
        r(""),
        f = null,
        function (e, t) {
            function n(e, t) {
                var n = e.createElement("p")
                    , r = e.getElementsByTagName("head")[0] || e.documentElement;
                return n.innerHTML = "x<style>" + t + "</style>",
                    r.insertBefore(n.lastChild, r.firstChild)
            }
            function r() {
                var e = g.elements;
                return "string" == typeof e ? e.split(" ") : e
            }
            function a(e) {
                var t = v[e[h]];
                return t || (t = {},
                    p++,
                    e[h] = p,
                    v[p] = t),
                    t
            }
            function o(e, n, r) {
                return n || (n = t),
                    s ? n.createElement(e) : (r || (r = a(n)),
                        (o = r.cache[e] ? r.cache[e].cloneNode() : m.test(e) ? (r.cache[e] = r.createElem(e)).cloneNode() : r.createElem(e)).canHaveChildren && !f.test(e) ? r.frag.appendChild(o) : o);
                var o
            }
            function c(e, n) {
                if (e || (e = t),
                    s)
                    return e.createDocumentFragment();
                for (var o = (n = n || a(e)).frag.cloneNode(), c = 0, i = r(), l = i.length; c < l; c++)
                    o.createElement(i[c]);
                return o
            }
            function i(e, t) {
                t.cache || (t.cache = {},
                    t.createElem = e.createElement,
                    t.createFrag = e.createDocumentFragment,
                    t.frag = t.createFrag()),
                    e.createElement = function (n) {
                        return g.shivMethods ? o(n, e, t) : t.createElem(n)
                    }
                    ,
                    e.createDocumentFragment = Function("h,f", "return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&&(" + r().join().replace(/\w+/g, function (e) {
                        return t.createElem(e),
                            t.frag.createElement(e),
                            'c("' + e + '")'
                    }) + ");return n}")(g, t.frag)
            }
            function l(e) {
                e || (e = t);
                var r = a(e);
                return g.shivCSS && !u && !r.hasCSS && (r.hasCSS = !!n(e, "article,aside,figcaption,figure,footer,header,hgroup,nav,section{display:block}mark{background:#FF0;color:#000}")),
                    s || i(e, r),
                    e
            }
            var u, s, d = e.html5 || {}, f = /^<|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i, m = /^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i, h = "_html5shiv", p = 0, v = {};
            !function () {
                try {
                    var e = t.createElement("a");
                    e.innerHTML = "<xyz></xyz>",
                        u = "hidden" in e,
                        s = 1 == e.childNodes.length || function () {
                            t.createElement("a");
                            var e = t.createDocumentFragment();
                            return "undefined" == typeof e.cloneNode || "undefined" == typeof e.createDocumentFragment || "undefined" == typeof e.createElement
                        }()
                } catch (d) {
                    u = !0,
                        s = !0
                }
            }();
            var g = {
                elements: d.elements || "abbr article aside audio bdi canvas data datalist details figcaption figure footer header hgroup mark meter nav output progress section summary time video",
                shivCSS: !1 !== d.shivCSS,
                supportsUnknownElements: s,
                shivMethods: !1 !== d.shivMethods,
                type: "default",
                shivDocument: l,
                createElement: o,
                createDocumentFragment: c
            };
            e.html5 = g,
                l(t)
        }(this, t),
        l._version = i,
        s.className = s.className.replace(/(^|\s)no-js(\s|$)/, "$1$2") + (u ? " js " + p.join(" ") : ""),
        l
}(0, this.document),
    Modernizr.addTest("xhr2", "FormData" in window);
