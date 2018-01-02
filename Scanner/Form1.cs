using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanner
{
    public partial class Form1 : Form
    {
        //单词符号及类别编码
        public enum Type {
            IDENTIFIER,//标识符
            CONST_INT, CONST_FLOAT,//整数和浮点数常量
            CONST_CHAR, CONST_STRING,//字符和字符串常量
            ADD, SUB, MULTIPLY, DEVIDE, MOD,// + - * / % 加减乘除取余
            INC, DEC,// ++ -- 自增自减
            ASSIGN,// = 赋值
            ADDEQ, SUBEQ, MULTIPLYEQ, DEVIDEEQ, MODEQ,// += -= *= /= %= 加减乘除与等于的连接
            GT, LT, EQ, GET, LET,// > < == >= <= 比较运算符
            AND_BIT, OR_BIT, NOT_BIT,// & | ^ 位运算符
            PARENTHESIS_R, PARENTHESIS_L, BRACKET_R, BRACKET_L, BRACES_R, BRACES_L,// ( ) [ ] { } 三种括号
            AND_LOGICAL, OR_LOGICAL, NOT_LOGICAL,// && || ! 逻辑运算符
            COMMA, DOT,COLON, SEMICOLON, QUESTION,// , . : ; ? 其他符号
            //关键字
            AUTO, BREAK, CASE, CHAR, CONST, CONTINUE, DEFAULT, DO,
            DOUBLE, ELSE, ENUM, EXTERN, FLOAT, FOR, GOTO, IF,
            INT, LONG, REGISTER, RETURN, SHORT ,SIGNED, SIZEOF, STATIC,
            STRUCT, SWITCH, TYPEDEF, UNION, UNSINGED, VOID, VOLATILE, WHILE
        };

        //关键字
        public Dictionary<string,Type> key = new Dictionary<string, Type> {
            { "auto", Type.AUTO }, { "break", Type.BREAK }, { "case", Type.CASE }, { "char",Type.CHAR },
            { "const", Type.CONST }, { "continue", Type.CONTINUE }, { "default", Type.DEFAULT }, { "do", Type.DO },
            { "double", Type.DOUBLE }, { "else", Type.ELSE }, { "enum", Type.ENUM }, { "extern", Type.EXTERN },
            { "float", Type.FLOAT }, { "for", Type.FOR }, { "goto", Type.GOTO }, { "if", Type.IF },
            { "int", Type.INT }, { "long", Type.LONG }, { "register", Type.REGISTER }, { "return", Type.RETURN },
            { "short", Type.SHORT }, { "signed", Type.SIGNED }, { "sizeof", Type.SIZEOF }, { "static", Type.STATIC },
            { "struct", Type.STRUCT }, { "switch", Type.SWITCH }, { "typedef", Type.TYPEDEF }, { "union", Type.UNION },
            { "unsigned", Type.UNSINGED }, { "void", Type.VOID }, { "volatile", Type.VOLATILE }, { "while", Type.WHILE }
        };
        
        //存储数据的结构
        public struct Data
        {
            public Type code;
            public string value;
            public Data(Type code, string value)
            {
                this.code = code;
                this.value = value;
            }
        }

        //扫描到的单词列表
        public Queue<Data> token = new Queue<Data>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            if (txbInput.Text.Equals(string.Empty))
            {
                return;
            }
            //按行遍历，方便跳过注释
            string[] lines = txbInput.Text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string keyword = string.Empty;    //识别的符号串
            int status = 0;//状态
            int length = 0;
            char current;
            int i = 0;
            foreach(string line in lines)
            {
                length = line.Length;
                for(i = 0; i < length; i++)
                {
                    current = line[i];
                    switch (status)
                    {
                        case 0:
                            status = currentStatus0(ref keyword, current);
                            break;
                        case 1:
                            status = currentStatus1(ref keyword, current, ref i);
                            break;
                        case 2:
                            status = currentStatus2(ref keyword, current, ref i);
                            break;
                        case 3:
                            status = currentStatus3(ref keyword, current, ref i);
                            break;
                        case 4:
                            status = currentStatus4(ref keyword, current, ref i);
                            break;
                        case 5:
                            status = currentStatus5(ref keyword, current, ref i);
                            break;
                        case 6:
                            status = currentStatus6(ref keyword, current, ref i);
                            break;
                        case 7:
                            status = currentStatus7(ref keyword, current, ref i, length);             
                            break;
                        case 8:
                            status = currentStatus8(ref keyword, current);
                            break;
                        case 9:
                            status = currentStatus9(ref keyword, current);
                            break;
                        case 10:
                            status = currentStatus10(ref keyword, current, ref i);
                            break;
                        case 11:
                            status = currentStatus11(ref keyword, current, ref i);
                            break;
                        case 12:
                            status = currentStatus12(ref keyword, current, ref i);
                            break;
                        default:
                            break;
                    }
                }
                //一行匹配结束
                if (!keyword.Equals(string.Empty))
                {
                    status = endOfLine(status, ref keyword);
                }
            }

            showResult();
        }

        private int currentStatus0(ref string keyword, char current)
        {
            int status = 0;
            //字母或下划线
            if ((current >= 'a' && current <= 'z') || (current >= 'A' && current <= 'Z') || current == '_')
            {
                status = 1;
            }
            //数字
            else if (current >= '0' && current <= '9')
            {
                status = 2;
            }
            else if (current == '+')
            {
                status = 4;
            }
            else if (current == '-')
            {
                status = 5;
            }
            else if (current == '*')
            {
                status = 6;
                keyword += current;
            }
            else if (current == '/')
            {
                status = 7;
            }
            else if (current == '>')
            {
                status = 10;
            }
            else if (current == '<')
            {
                status = 11;
            }
            else if (current == '=')
            {
                status = 12;
            }
            else if ("()[]{}.,;?:".ToCharArray().Contains<char>(current))
            {
                Type type = new Type();
                switch (current)
                {
                    case '(':
                        type = Type.PARENTHESIS_L;
                        break;
                    case ')':
                        type = Type.PARENTHESIS_R;
                        break;
                    case '[':
                        type = Type.BRACKET_L;
                        break;
                    case ']':
                        type = Type.BRACKET_R;
                        break;
                    case '{':
                        type = Type.BRACES_L;
                        break;
                    case '}':
                        type = Type.BRACES_R;
                        break;
                    case '.':
                        type = Type.DOT;
                        break;
                    case ',':
                        type = Type.COMMA;
                        break;
                    case ';':
                        type = Type.SEMICOLON;
                        break;
                    case '?':
                        type = Type.QUESTION;
                        break;
                    case ':':
                        type = Type.COLON;
                        break;
                }
                token.Enqueue(new Data(type, current.ToString()));
                return 0;
            }
            //忽略其他字符
            else
            {
                return status;
            }
            keyword += current;
            return status;
        }

        private int currentStatus1(ref string keyword, char current, ref int i)
        {
            int status = 1;
            //字母数字下划线
            if ((current >= 'a' && current <= 'z') || (current >= 'A' && current <= 'Z') ||
                (current >= '0' && current <= '9') || current == '_')
            {
                keyword += current;
            }
            else
            {
                //关键字
                if (key.ContainsKey(keyword))
                {
                    token.Enqueue(new Data(key[keyword], keyword));
                }
                //标识符
                else
                {
                    token.Enqueue(new Data(Type.IDENTIFIER, keyword));
                }
                keyword = string.Empty;
                i--;
                status = 0;
            }
            return status;
        }

        private int currentStatus2(ref string keyword, char current, ref int i)
        {
            int status = 2;
            //数字
            if (current >= '0' && current <= '9')
            {
                keyword += current;
            }
            //点
            else if (current == '.')
            {
                status = 3;
                keyword += current;
            }
            //整数匹配结束
            else
            {
                token.Enqueue(new Data(Type.CONST_INT, keyword));
                keyword = string.Empty;
                status = 0;
                i--;
            }
            return status;
        }

        private int currentStatus3(ref string keyword, char current, ref int i)
        {
            int status = 3;
            if (current >= '0' && current <= '9')
            {
                keyword += current;
            }
            //小数匹配结束
            else
            {
                token.Enqueue(new Data(Type.CONST_FLOAT, keyword));
                keyword = string.Empty;
                status = 0;
                i--;
            }
            return status;
        }

        private int currentStatus4(ref string keyword, char current, ref int i)
        {
            //++
            if (current == '+')
            {
                token.Enqueue(new Data(Type.INC, keyword + current));
            }
            //+=
            else if (current == '=')
            {
                token.Enqueue(new Data(Type.ADDEQ, keyword + current));

            }
            //+
            else
            {
                token.Enqueue(new Data(Type.ADD, keyword));
                i--;
            }
            keyword = string.Empty;
            return 0;
        }

        private int currentStatus5(ref string keyword, char current, ref int i)
        {
            //--
            if (current == '-')
            {
                token.Enqueue(new Data(Type.DEC, keyword + current));
            }
            //-=
            else if (current == '=')
            {
                token.Enqueue(new Data(Type.SUBEQ, keyword + current));

            }
            //-
            else
            {
                token.Enqueue(new Data(Type.SUB, keyword));
                i--;
            }
            keyword = string.Empty;
            return 0;
        }

        private int currentStatus6(ref string keyword, char current, ref int i)
        {
            // *=
            if (current == '=')
            {
                token.Enqueue(new Data(Type.MULTIPLYEQ, keyword + current));
            }
            // *
            else
            {
                token.Enqueue(new Data(Type.MULTIPLY, keyword));
                i--;
            }
            keyword = string.Empty;
            return 0;
        }

        private int currentStatus7(ref string keyword, char current, ref int i, int length)
        {
            int status = 0;
            // /=
            if (current == '=')
            {
                token.Enqueue(new Data(Type.DEVIDEEQ, keyword + current));
                keyword = string.Empty;
            }
            //单行注释
            else if (current == '/')
            {
                //下标移到行尾
                keyword = string.Empty;
                i = length;
            }
            // 区块注释/*
            else if (current == '*')
            {
                keyword += current;
                status = 8;
            }
            // /
            else
            {
                token.Enqueue(new Data(Type.DEVIDE, keyword));
                keyword = string.Empty;
                status = 0;
                i--;
            }
            return status;
        }

        private int currentStatus8(ref string keyword, char current)
        {
            int status = 8;
            if (current == '*')
            {
                keyword += current;
                status = 9;
            }
            return status;
        }

        private int currentStatus9(ref string keyword, char current)
        {
            int status = 9;
            //区块注释结束
            if (current == '/')
            {
                keyword = string.Empty;
                status = 0;
            }
            else if (current != '*')
            {
                status = 8;
            }
            return status;
        }

        private int currentStatus10(ref string keyword, char current, ref int i)
        {
            // >=
            if (current == '=')
            {
                token.Enqueue(new Data(Type.GET, keyword + current));
            }
            // >
            else
            {
                token.Enqueue(new Data(Type.GT, keyword));
                i--;
            }
            keyword = string.Empty;
            return 0;
        }

        private int currentStatus11(ref string keyword, char current, ref int i)
        {
            // <=
            if (current == '=')
            {
                token.Enqueue(new Data(Type.LET, keyword + current));
            }
            // <
            else
            {
                token.Enqueue(new Data(Type.LT, keyword));
                i--;
            }
            keyword = string.Empty;
            return 0;
        }

        private int currentStatus12(ref string keyword, char current, ref int i)
        {
            // ==
            if (current == '=')
            {
                token.Enqueue(new Data(Type.EQ, keyword + current));
            }
            // =
            else
            {
                token.Enqueue(new Data(Type.ASSIGN, keyword));
                i--;
            }
            keyword = string.Empty;
            return 0;
        }

        private int endOfLine(int status, ref string keyword)
        {
            if (status == 9 || status == 8)
            {
                return 8;
            }
            Type type = new Type();
            switch (status)
            {
                case 1:
                    type = Type.IDENTIFIER;
                    break;
                case 2:
                    type = Type.CONST_INT;
                    break;
                case 3:
                    type = Type.CONST_FLOAT;
                    break;
                case 4:
                    type = Type.ADD;
                    break;
                case 5:
                    type = Type.SUB;
                    break;
                case 6:
                    type = Type.MULTIPLY;
                    break;
                case 7:
                    type = Type.DEVIDE;
                    break;
                case 10:
                    type = Type.GT;
                    break;
                case 11:
                    type = Type.LT;
                    break;
                case 12:
                    type = Type.EQ;
                    break;
                default:
                    break;
            }
            token.Enqueue(new Data(type, keyword));
            keyword = string.Empty;
            return 0;
        }

        private void showResult()
        {
            txbOutput.Text = string.Empty;

            Data data;
            while(token.Count > 0)
            {
                data = token.Dequeue();
                txbOutput.Text += data.value + "," + (int)data.code + "\r\n";
            }
            return;
        }
    }
}
