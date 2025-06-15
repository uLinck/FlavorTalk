import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class FlavorActionButton extends StatelessWidget {
  const FlavorActionButton({
    required this.text,
    super.key,
    this.onPressed,
    this.applyPadding = false,
    this.outlined = false,
  });

  final String text;
  final void Function()? onPressed;
  final bool applyPadding;
  final bool outlined;

  @override
  Widget build(BuildContext context) {
    if (applyPadding) {
      return Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16),
        child: button(context),
      );
    }

    return button(context);
  }

  ElevatedButton button(BuildContext context) => ElevatedButton(
    style: ButtonStyle(
      backgroundColor: outlined ? null : WidgetStateProperty.all(Colors.black),
      minimumSize: WidgetStateProperty.all(
        Size(MediaQuery.of(context).size.width, 48),
      ),
      shape: WidgetStateProperty.all(
        RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(8),
          side: outlined ? const BorderSide() : BorderSide.none,
        ),
      ),
    ),
    onPressed: onPressed,
    child: Text(
      text,
      style: GoogleFonts.poppins(color: outlined ? Colors.black : Colors.white),
    ),
  );
}
