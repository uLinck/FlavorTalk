import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class FlavorField extends StatelessWidget {
  const FlavorField({
    required this.label,
    required this.hint,
    required this.onChanged,
    super.key,
    this.focusNode,
  });

  final String label;
  final String hint;
  final FocusNode? focusNode;
  final void Function(String) onChanged;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        spacing: 4,
        children: [
          Text(
            label,
              style: GoogleFonts.poppins(
                decoration: TextDecoration.none,
                color: Colors.black,
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
          ),
          TextField(
            focusNode: focusNode,
            onChanged: onChanged,
            decoration: InputDecoration(
              hintText: hint,
              hintStyle: GoogleFonts.poppins(
                decoration: TextDecoration.none,
                color: Colors.black,
                fontSize: 15,
                fontWeight: FontWeight.w300,
              ),
              border:const OutlineInputBorder(
                borderRadius: BorderRadius.all(Radius.circular(8)),
              ),
              floatingLabelBehavior: FloatingLabelBehavior.never,
            ),
          ),
        ],
      ),
    );
  }
}
