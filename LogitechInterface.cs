using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LogitechInterface
{
    /// <summary>
    ///     A C# interface for the LCD and the buttons on Logitech G-Series Devices. Supports all devices up to the G19. To use, create a new instance of
    ///     <see cref="LogitechLcd"/> and call <see cref="LcdUpdate"/> every frame in your main loop.
    /// </summary>
    /// <author email="alexmikhalevalex@gmail.com">Alex Mikhalev</author>
    public class LogitechLcd
    {
        private static class NativeMethods
        {
            #region Supplied Constants

            // LCD types
// ReSharper disable InconsistentNaming
            public const int LOGI_LCD_TYPE_MONO = 0x00000001;
            public const int LOGI_LCD_TYPE_COLOR = 0x00000002;

            // Monochrome LCD buttons
            public const int LOGI_LCD_MONO_BUTTON_0 = 0x00000001;
            public const int LOGI_LCD_MONO_BUTTON_1 = 0x00000002;
            public const int LOGI_LCD_MONO_BUTTON_2 = 0x00000004;
            public const int LOGI_LCD_MONO_BUTTON_3 = 0x00000008;

            // Color LCD buttons
            public const int LOGI_LCD_COLOR_BUTTON_LEFT = 0x00000100;
            public const int LOGI_LCD_COLOR_BUTTON_RIGHT = 0x00000200;
            public const int LOGI_LCD_COLOR_BUTTON_OK = 0x00000400;
            public const int LOGI_LCD_COLOR_BUTTON_CANCEL = 0x00000800;
            public const int LOGI_LCD_COLOR_BUTTON_UP = 0x00001000;
            public const int LOGI_LCD_COLOR_BUTTON_DOWN = 0x00002000;
            public const int LOGI_LCD_COLOR_BUTTON_MENU = 0x00004000;

            // Monochrome LCD size
            public const int LOGI_LCD_MONO_WIDTH = 160;
            public const int LOGI_LCD_MONO_HEIGHT = 43;

            // Color LCD size
            public const int LOGI_LCD_COLOR_WIDTH = 320;
            public const int LOGI_LCD_COLOR_HEIGHT = 240;
            // ReSharper restore InconsistentNaming

            #endregion

            #region Internal Functions

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdInit([MarshalAs(UnmanagedType.LPWStr)] string freindlyName, int lcdtype);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdIsConnected(int lcdtype);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdIsButtonPressed(int button);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void LogiLcdUpdate();

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void LogiLcdShutdown();

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdMonoSetBackground(byte[] monoBitmap);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdMonoSetText(int lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdColorSetBackground(byte[] colorBitmap);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdColorSetTitle([MarshalAs(UnmanagedType.LPWStr)] string text, int red,
                                                           int green, int blue);

            [DllImport("LogitechLcd.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool LogiLcdColorSetText(int lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text,
                                                          int red, int green, int blue);

            #endregion
        }

        #region Enums and Constants

        /// <summary>
        ///     An enum of buttons on Logitech G-Series devices
        /// </summary>
        public enum LcdButton
        {
            /// <summary>
            ///     Monochrome LCD button 1
            /// </summary>
            Monochrome0 = NativeMethods.LOGI_LCD_MONO_BUTTON_0,

            /// <summary>
            ///     Monochrome LCD button 2
            /// </summary>
            Monochrome1 = NativeMethods.LOGI_LCD_MONO_BUTTON_1,

            /// <summary>
            ///     Monochrome LCD button 3
            /// </summary>
            Monochrome2 = NativeMethods.LOGI_LCD_MONO_BUTTON_2,

            /// <summary>
            ///     Monochrome LCD button 4
            /// </summary>
            Monochrome3 = NativeMethods.LOGI_LCD_MONO_BUTTON_3,

            /// <summary>
            ///     Color LCD up button
            /// </summary>
            ColorUp = NativeMethods.LOGI_LCD_COLOR_BUTTON_UP,

            /// <summary>
            ///     Color LCD down button
            /// </summary>
            ColorDown = NativeMethods.LOGI_LCD_COLOR_BUTTON_DOWN,

            /// <summary>
            ///     Color LCD left button
            /// </summary>
            ColorLeft = NativeMethods.LOGI_LCD_COLOR_BUTTON_LEFT,

            /// <summary>
            ///     Color LCD right button
            /// </summary>
            ColorRight = NativeMethods.LOGI_LCD_COLOR_BUTTON_RIGHT,

            /// <summary>
            ///     Color LCD ok button
            /// </summary>
            ColorOk = NativeMethods.LOGI_LCD_COLOR_BUTTON_OK,

            /// <summary>
            ///     Color LCD cancel button
            /// </summary>
            ColorCancel = NativeMethods.LOGI_LCD_COLOR_BUTTON_CANCEL,

            /// <summary>
            ///     Color LCD menu button
            /// </summary>
            ColorMenu = NativeMethods.LOGI_LCD_COLOR_BUTTON_MENU
        }

        /// <summary>
        ///     An enum of Logitech LCD Types on Logitech G-Series Devices
        /// </summary>
        public enum LcdType
        {
            /// <summary>
            ///     Logitech Monochome LCD (Logitech G510, G13, G15 v1 and v2)
            /// </summary>
            Monochrome = NativeMethods.LOGI_LCD_TYPE_MONO,

            /// <summary>
            ///     Logitech Color LCD (Logitech G19)
            /// </summary>
            Color = NativeMethods.LOGI_LCD_TYPE_COLOR,

            /// <summary>
            ///     Both Logitech color and monochrome LCD's
            /// </summary>
            Both = NativeMethods.LOGI_LCD_TYPE_COLOR | NativeMethods.LOGI_LCD_TYPE_MONO
        }

        /// <summary>
        ///     The width, in pixles, of all Logitech monochrome LCD's
        /// </summary>
        public const int MonochromeLcdWidth = NativeMethods.LOGI_LCD_MONO_WIDTH;

        /// <summary>
        ///     The height, in pixles, of all Logitech monochrome LCD's
        /// </summary>
        public const int MonochromeLcdHeight = NativeMethods.LOGI_LCD_MONO_HEIGHT;

        /// <summary>
        ///     The width, in pixles, of all Logitech color LCD's
        /// </summary>
        public const int ColorLcdWidth = NativeMethods.LOGI_LCD_COLOR_WIDTH;

        /// <summary>
        ///     The height, in pixles, of all Logitech color LCD's
        /// </summary>
        public const int ColorLcdHeight = NativeMethods.LOGI_LCD_COLOR_HEIGHT;

        #endregion

        #region Functions

        /// <summary>
        ///     Initializes the connection to the Logitech G-Series device. Must be called before any other function in this library
        /// </summary>
        /// <param name="freindlyName">The human-readable name of the LCD applet. Cannot be changed after initialization</param>
        /// <param name="lcdType">
        ///     The type of LCD to initialize to. Can be one of the following:
        ///     <list type="bullet">
        ///         <item>
        ///             <term>
        ///                 <see cref="LcdType.Monochrome" />
        ///             </term>
        ///             <description>Only Logitech Monochrome LCD devices (G510, G13, G15 v1 and v2)</description>
        ///         </item>
        ///         <item>
        ///             <term>
        ///                 <see cref="LcdType.Color" />
        ///             </term>
        ///             <description>Only Logitech Color LCD devices (G19)</description>
        ///         </item>
        ///         <item>
        ///             <term>
        ///                 <see cref="LcdType.Both" />
        ///             </term>
        ///             <description>Both color and monochrome LCD devices</description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <seealso cref="LcdType" />
        /// <returns>True if successful, false if not</returns>
        public LogitechLcd(string freindlyName, LcdType lcdType)
        {
            DynamicLoading.EnableDynamicLoading("LogitechInterface", "x64");
            if (!NativeMethods.LogiLcdInit(freindlyName, (int) lcdType))
            {
                throw new ExternalException("Error initializing LCD");
            }
        }

        /// <summary>
        ///     Checks if a Logitech G-Series device is connected
        /// </summary>
        /// <param name="lcdType">
        ///     The type of LCD to check the connection to. Can be one of the following:
        ///     <list type="bullet">
        ///         <item>
        ///             <term>
        ///                 <see cref="LcdType.Monochrome" />
        ///             </term>
        ///             <description>Only Logitech Monochrome LCD devices (G510, G13, G15 v1 and v2)</description>
        ///         </item>
        ///         <item>
        ///             <term>
        ///                 <see cref="LcdType.Color" />
        ///             </term>
        ///             <description>Only Logitech Color LCD devices (G19)</description>
        ///         </item>
        ///         <item>
        ///             <term>
        ///                 <see cref="LcdType.Both" />
        ///             </term>
        ///             <description>Both color and monochrome LCD devices</description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <seealso cref="LcdType" />
        /// <returns>True if a specified device is connected, false if not</returns>
        public bool LcdIsConnected(LcdType lcdType)
        {
            return NativeMethods.LogiLcdIsConnected((int) lcdType);
        }

        /// <summary>
        ///     Checks if a specified button on a Logitech G-Series device is pressed
        /// </summary>
        /// <param name="button">
        ///     The button to check the state of
        /// </param>
        /// <seealso cref="LcdButton" />
        /// <returns>True if the specified button is pressed, false if not</returns>
        public bool LcdIsButtonPressed(LcdButton button)
        {
            return NativeMethods.LogiLcdIsButtonPressed((int) button);
        }

        /// <summary>
        ///     Updates the LCD display on a Logitech G-Series device. Should be called every frame in your main loop.
        /// </summary>
        public void LcdUpdate()
        {
            NativeMethods.LogiLcdUpdate();
        }

        /// <summary>
        ///     Kills the applet and frees the memory used.
        /// </summary>
        public void LcdShutdown()
        {
            NativeMethods.LogiLcdShutdown();
        }

        /// <summary>
        ///     Destroy
        /// </summary>
        ~LogitechLcd()
        {
            LcdShutdown();
        }

        /// <summary>
        ///     Sets the specified bitmap image as the background on the monochrome LCD
        /// </summary>
        /// <param name="monochromBitmap">
        ///     The array of pixles to display on the device. It is organized as a rectangular area, 160 bytes wide
        ///     and 43 bytes high. 255 is white and 0 is black. The array should be 6,880 bytes long.
        /// </param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdMonoSetBackground(byte[] monochromBitmap)
        {
            if (monochromBitmap.Length != MonochromeLcdWidth*MonochromeLcdHeight)
                throw new ArgumentException("Array size invalid");

            return NativeMethods.LogiLcdMonoSetBackground(monochromBitmap);
        }

        /// <summary>
        ///     Sets the specified bitmap image as the background on the monochrome lcd
        /// </summary>
        /// <param name="monochromeBitmap">
        ///     The <see cref="Bitmap" /> image to display on the LCD
        /// </param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdMonoSetBackground(Bitmap monochromeBitmap)
        {
            if (monochromeBitmap == null)
                throw new ArgumentNullException("monochromeBitmap");
            if (monochromeBitmap.Width != MonochromeLcdWidth || monochromeBitmap.Height != MonochromeLcdHeight)
                throw new ArgumentException("Invalid bitmap size", "monochromeBitmap");
            var byteMap = new byte[MonochromeLcdWidth * MonochromeLcdHeight];
            for (int x = 0; x < MonochromeLcdWidth; x++)
            {
                for (int y = 0; y < MonochromeLcdHeight; y++)
                {
                    byteMap[x*MonochromeLcdHeight + y] = (byte) Math.Floor(monochromeBitmap.GetPixel(x, y).GetBrightness());
                }
            }
            return LcdMonoSetBackground(byteMap);
        }

        /// <summary>
        ///     Sets the text displayed on a specific line on the display
        /// </summary>
        /// <param name="lineNumber">The line on the screen that the text will appear on. There are 4 lines, so this can be 0-3.</param>
        /// <param name="text">The text to display</param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdMonoSetText(int lineNumber, string text)
        {
            if (lineNumber < 0 || lineNumber > 3)
                throw new ArgumentOutOfRangeException("lineNumber", "The line number must be from 0 - 3");
            return NativeMethods.LogiLcdMonoSetText(lineNumber, text);
        }

        /// <summary>
        ///     Sets the specified bitmap image as the background on the color LCd
        /// </summary>
        /// <param name="colorBitmap">
        ///     The array of pixles to display on the device. 320 bytes wide by 240 bytes high. Since the color
        ///     LCD can display full color, 4 bytes per picture are used. The size of the bitmap is 320x240x4 = 307200. The bytes are arranged like this:
        ///     <para> Pixel 0, 0   ... Pixel 319, 0   </para>
        ///     <para>   B G R A    ...   B G R A      </para>
        ///     <para>     ...      ...      ...        </para>
        ///     <para> Pixel 0, 239 ... Pixel 319, 239 </para>
        ///     <para>   B G R A    ...   B G R A      </para>
        ///     <para>B - Blue 0 - 255</para>
        ///     <para>G - Green 0 - 255</para>
        ///     <para>R - Red 0 - 255</para>
        ///     A - Alpha 0 - 255
        /// </param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdColorSetBackground(byte[] colorBitmap)
        {
            if (colorBitmap.Length != ColorLcdWidth*ColorLcdHeight*4)
                throw new ArgumentException("Array size invalid. Should be 307,200 , but is " + colorBitmap.Length,
                                            "colorBitmap");
            return NativeMethods.LogiLcdColorSetBackground(colorBitmap);
        }

        /// <summary>
        ///     Sets the specified bitmap image as the background on the color LCd
        /// </summary>
        /// <param name="colorBitmap">
        ///     The <see cref="Bitmap" /> to display
        /// </param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdColorSetBackground(Bitmap colorBitmap)
        {
            if (colorBitmap == null)
                throw new ArgumentNullException("colorBitmap");
            if (colorBitmap.Width != ColorLcdWidth || colorBitmap.Height != ColorLcdHeight)
                throw new ArgumentException("Invalid bitmap size", "colorBitmap");
            var byteMap = new byte[ColorLcdWidth*ColorLcdHeight*4];
            for (int x = 0; x < ColorLcdWidth; x++)
            {
                for (int y = 0; y < ColorLcdHeight; y++)
                {
                    Color color = colorBitmap.GetPixel(x, y);
                    byteMap[(x*ColorLcdHeight + y)*4] = color.B;
                    byteMap[(x*ColorLcdHeight + y)*4 + 1] = color.G;
                    byteMap[(x*ColorLcdHeight + y)*4 + 2] = color.R;
                    byteMap[(x*ColorLcdHeight + y)*4 + 3] = color.A;
                }
            }
            return LcdColorSetBackground(byteMap);
        }

        /// <summary>
        ///     Sets the specified text in the first line on the color LCD device. The font size will be larger than the one used on the other lines.
        /// </summary>
        /// <param name="text">The text to be displayed</param>
        /// <param name="red">The red component of the color of the text, 0 - 255</param>
        /// <param name="green">The green component of the color of the text, 0 - 255</param>
        /// <param name="blue">The blue component of the color of the text, 0 - 255</param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdColorSetTitle(string text, int red, int green, int blue)
        {
            if (red < 0 || red > 255)
                throw new ArgumentOutOfRangeException("red", "Red must be 0 - 255");
            if (green < 0 || green > 255)
                throw new ArgumentOutOfRangeException("green", "Green must be 0 - 255");
            if (blue < 0 || blue > 255)
                throw new ArgumentOutOfRangeException("blue", "lue must be 0 - 255");
            return NativeMethods.LogiLcdColorSetTitle(text, red, green, blue);
        }

        /// <summary>
        ///     Sets the specified text in the first line on the color LCD device. The font size will be larger than the one used on the other lines.
        /// </summary>
        /// <param name="text">The text to be displayed</param>
        /// <param name="color">The color of the text</param>
        /// <returns>True if successful, false if not</returns>
        public bool LcdColorSetTitle(string text, Color color)
        {
            if (color == null)
                throw new ArgumentNullException("color");
            return LcdColorSetTitle(text, color.R, color.G, color.B);
        }

        /// <summary>
        ///     Sets the specified text on the requested line on the color LCD device.
        /// </summary>
        /// <param name="lineNumber">The line on the screen that the text will appear on. There are 8 lines, so this is 0-7</param>
        /// <param name="text">The text to be displayed</param>
        /// <param name="red">The red component of the color of the text, 0 - 255</param>
        /// <param name="green">The green component of the color of the text, 0 - 255</param>
        /// <param name="blue">The blue component of the color of the text, 0 - 255</param>
        /// <returns></returns>
        public bool LcdColorSetText(int lineNumber, string text, int red, int green, int blue)
        {
            if (lineNumber < 0 || lineNumber > 7)
                throw new ArgumentOutOfRangeException("lineNumber", "The line number must be from 0 - 7");
            if (red < 0 || red > 255)
                throw new ArgumentOutOfRangeException("red", "Red must be 0 - 255");
            if (green < 0 || green > 255)
                throw new ArgumentOutOfRangeException("green", "Green must be 0 - 255");
            if (blue < 0 || blue > 255)
                throw new ArgumentOutOfRangeException("blue", "Blue must be 0 - 255");
            return NativeMethods.LogiLcdColorSetText(lineNumber, text, red, green, blue);
        }

        /// <summary>
        ///     Sets the specified text on the requested line on the color LCD device.
        /// </summary>
        /// <param name="lineNumber">The line on the screen that the text will appear on. There are 8 lines, so this is 0-7</param>
        /// <param name="text">The text to be displayed</param>
        /// <param name="color">The color of the text</param>
        /// <returns>True if successful, false if no</returns>
        public bool LcdColorSetText(int lineNumber, string text, Color color)
        {
            if (lineNumber < 0 || lineNumber > 7)
                throw new ArgumentOutOfRangeException("lineNumber", "The line number must be from 0 - 7");
            if (color == null)
                throw new ArgumentNullException("color");
            return LcdColorSetText(lineNumber, text, color.R, color.G, color.B);
        }

        #endregion
    }
}